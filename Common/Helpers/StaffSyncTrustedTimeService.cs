
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public sealed class StaffSyncTrustedTimeService
    {
        // ---------------------------------------------------------
        // SHARED INSTANCE
        // ---------------------------------------------------------

        private static readonly Lazy<StaffSyncTrustedTimeService> _instance =
            new Lazy<StaffSyncTrustedTimeService>(
                () => new StaffSyncTrustedTimeService());

        public static StaffSyncTrustedTimeService objTrustedTime
        {
            get { return _instance.Value; }
        }

        // ---------------------------------------------------------
        // CONFIGURATION
        // ---------------------------------------------------------

        private readonly int MinimumRequiredSources = 2;
        private readonly double AllowedDifferenceSeconds = 8;

        private readonly int MaximumAttempts = 2;
        private readonly int RetryDelayMilliseconds = 1500;

        private readonly string TimeZoneId;

        private static readonly HttpClient httpClient =
            CreateHttpClient();

        private static HttpClient CreateHttpClient()
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            client.Timeout = TimeSpan.FromSeconds(10);

            return client;
        }

        // ---------------------------------------------------------
        // TIME SOURCES
        // ---------------------------------------------------------

        // Use lightweight endpoints instead of provider homepages.
        // All three providers must return a successful HTTPS response
        // containing a valid HTTP Date header.
        private readonly string[] TimeSources =
        {
            "https://www.google.com/generate_204",
            "https://captive.apple.com/hotspot-detect.html",
            "https://www.cloudflare.com/cdn-cgi/trace"
        };

        // ---------------------------------------------------------
        // CONSTRUCTOR
        // ---------------------------------------------------------

        private StaffSyncTrustedTimeService()
        {
            TimeZoneId =
                ConfigurationManager.AppSettings["StaffSyncTimeZone"];

            if (string.IsNullOrWhiteSpace(TimeZoneId))
            {
                TimeZoneId = "India Standard Time";
            }
        }

        // ---------------------------------------------------------
        // MODEL: TIME SAMPLE
        // ---------------------------------------------------------

        public sealed class TimeSample
        {
            public DateTimeOffset UtcTime { get; set; }

            public string Source { get; set; }
        }

        // ---------------------------------------------------------
        // MODEL: TRUSTED TIME RESULT
        // ---------------------------------------------------------

        public sealed class TrustedTimeResult
        {
            public DateTimeOffset UtcTime { get; set; }

            public int SuccessfulSourceCount { get; set; }

            public List<string> SuccessfulSources { get; set; }

            public DateTime RetrievedAtUtc { get; set; }
        }

        // ---------------------------------------------------------
        // GET TRUSTED TIME
        // ---------------------------------------------------------

        public async Task<TrustedTimeResult> GetTime()
        {
            List<string> errors = new List<string>();

            for (int attempt = 1;
                 attempt <= MaximumAttempts;
                 attempt++)
            {
                ConcurrentBag<string> attemptErrors =
                    new ConcurrentBag<string>();

                List<Task<TimeSample>> requests =
                    new List<Task<TimeSample>>();

                foreach (string source in TimeSources)
                {
                    requests.Add(
                        GetTimeFromSourceAsync(source, attemptErrors));
                }

                TimeSample[] results =
                    await Task.WhenAll(requests);

                List<TimeSample> validSamples =
                    results
                    .Where(x => x != null)
                    .OrderBy(x => x.UtcTime)
                    .ToList();

                errors.AddRange(attemptErrors);

                if (validSamples.Count >= MinimumRequiredSources)
                {
                    List<TimeSample> matchingSamples =
                        FindMatchingSamples(validSamples);

                    if (matchingSamples.Count >= MinimumRequiredSources)
                    {
                        DateTimeOffset trustedUtcTime =
                            GetMedianTime(matchingSamples);

                        return new TrustedTimeResult
                        {
                            UtcTime = trustedUtcTime,

                            SuccessfulSourceCount =
                                matchingSamples.Count,

                            SuccessfulSources =
                                matchingSamples
                                .Select(x => x.Source)
                                .ToList(),

                            RetrievedAtUtc =
                                DateTime.UtcNow
                        };
                    }

                    errors.Add(
                        "Available time sources did not agree within " +
                        AllowedDifferenceSeconds + " seconds.");
                }
                else
                {
                    errors.Add(
                        "Only " + validSamples.Count +
                        " time source(s) responded successfully.");
                }

                if (attempt < MaximumAttempts)
                {
                    await Task.Delay(RetryDelayMilliseconds);
                }
            }

            throw new Exception(
                "Unable to obtain trusted date and time after " +
                MaximumAttempts + " attempts.\n\n" +
                "At least " + MinimumRequiredSources +
                " time sources must respond successfully and agree.\n\n" +
                string.Join("\n", errors.Distinct()));
        }

        // ---------------------------------------------------------
        // GET TIME FROM INDIVIDUAL SOURCE
        // ---------------------------------------------------------

        private async Task<TimeSample> GetTimeFromSourceAsync(
            string source,
            ConcurrentBag<string> errors)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                using (HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, source))
                {
                    request.Headers.CacheControl =
                        new CacheControlHeaderValue
                        {
                            NoCache = true,
                            NoStore = true
                        };

                    request.Headers.Pragma.ParseAdd("no-cache");
                    request.Headers.UserAgent.ParseAdd("StaffSync/1.0");

                    using (HttpResponseMessage response =
                        await httpClient.SendAsync(
                            request,
                            HttpCompletionOption.ResponseHeadersRead))
                    {
                        stopwatch.Stop();

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new HttpRequestException(
                                "HTTP " +
                                (int)response.StatusCode + " (" +
                                response.ReasonPhrase + ").");
                        }

                        DateTimeOffset? serverDate =
                            response.Headers.Date;

                        if (!serverDate.HasValue)
                        {
                            throw new Exception(
                                "HTTP Date header was not available.");
                        }

                        // HTTP Date headers have one-second precision.
                        // Add half the request duration to approximate
                        // the time at the midpoint of the request.
                        DateTimeOffset estimatedUtcTime =
                            serverDate.Value.ToUniversalTime().Add(
                                TimeSpan.FromMilliseconds(
                                    stopwatch.Elapsed.TotalMilliseconds / 2));

                        return new TimeSample
                        {
                            UtcTime = estimatedUtcTime,
                            Source = source
                        };
                    }
                }
            }
            catch (TaskCanceledException)
            {
                errors.Add(
                    source + ": Request timed out after " +
                    httpClient.Timeout.TotalSeconds + " seconds.");

                return null;
            }
            catch (Exception ex)
            {
                errors.Add(source + ": " + ex.Message);

                return null;
            }
        }

        // ---------------------------------------------------------
        // FIND AGREEMENT BETWEEN TIME SOURCES
        // ---------------------------------------------------------

        private List<TimeSample> FindMatchingSamples(
            List<TimeSample> validSamples)
        {
            List<TimeSample> matchingSamples = null;

            for (int i = 0; i < validSamples.Count; i++)
            {
                DateTimeOffset referenceTime =
                    validSamples[i].UtcTime;

                List<TimeSample> currentGroup =
                    validSamples
                    .Where(x =>
                        Math.Abs(
                            (x.UtcTime - referenceTime).TotalSeconds)
                        <= AllowedDifferenceSeconds)
                    .ToList();

                if (matchingSamples == null ||
                    currentGroup.Count > matchingSamples.Count)
                {
                    matchingSamples = currentGroup;
                }
            }

            return matchingSamples ?? new List<TimeSample>();
        }

        // ---------------------------------------------------------
        // GET MEDIAN TIME
        // ---------------------------------------------------------

        private DateTimeOffset GetMedianTime(
            List<TimeSample> matchingSamples)
        {
            List<long> ticks =
                matchingSamples
                .Select(x => x.UtcTime.UtcTicks)
                .OrderBy(x => x)
                .ToList();

            int count = ticks.Count;

            long medianTicks;

            if (count % 2 == 0)
            {
                medianTicks =
                    ticks[count / 2 - 1] +
                    ((ticks[count / 2] -
                      ticks[count / 2 - 1]) / 2);
            }
            else
            {
                medianTicks = ticks[count / 2];
            }

            return new DateTimeOffset(
                medianTicks,
                TimeSpan.Zero);
        }

        // ---------------------------------------------------------
        // GET LATEST TRUSTED LOCAL DATE AND TIME
        // ---------------------------------------------------------

        public async Task<DateTime> GetCurrentLocalDateTime()
        {
            TrustedTimeResult trustedTime =
                await GetTime();

            return GetLocalDateTime(trustedTime);
        }

        // ---------------------------------------------------------
        // CONVERT TRUSTED TIME TO LOCAL TIME
        // ---------------------------------------------------------

        public DateTime GetLocalDateTime(
            TrustedTimeResult trustedTime)
        {
            if (trustedTime == null)
            {
                throw new ArgumentNullException(
                    "trustedTime");
            }

            TimeZoneInfo timeZone =
                TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);

            DateTime localTime =
                TimeZoneInfo.ConvertTime(
                    trustedTime.UtcTime,
                    timeZone).DateTime;

            return DateTime.SpecifyKind(
                localTime,
                DateTimeKind.Unspecified);
        }

        // ---------------------------------------------------------
        // GET UTC DATE AND TIME FOR DATABASE STORAGE
        // ---------------------------------------------------------

        public DateTime GetAccessUtcDateTime(
            TrustedTimeResult trustedTime)
        {
            if (trustedTime == null)
            {
                throw new ArgumentNullException(
                    "trustedTime");
            }

            return trustedTime.UtcTime.UtcDateTime;
        }

        // ---------------------------------------------------------
        // GET CONFIGURED TIME ZONE
        // ---------------------------------------------------------

        public string GetConfiguredTimeZone()
        {
            return TimeZoneId;
        }
    }
}
