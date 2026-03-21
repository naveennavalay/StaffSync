using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using ColorMode = DinkToPdf.ColorMode;

namespace StaffSync
{
    public class myMailService
    {

        DALStaffSync.clsAppSettings objAppSettings = new DALStaffSync.clsAppSettings();

        //private string _fromEmail;
        //private string _appPassword;

        //public void MailService(string fromEmail, string appPassword)
        //{
        //    _fromEmail = fromEmail;
        //    _appPassword = appPassword;
        //}

        public void SendMail(string toMailID, string subject, string mailTemplate, Dictionary<string, string> dictData = null, bool isReplaceRequired = true, bool includeLogo = false, byte[] logoBytes = null, string logoContentId = "companylogo")
        {
            try
            {
                if (Convert.ToBoolean(objAppSettings.GetSpecificAppSettingsInfo("Mail Notification Enabled").AppSettingValue.ToString()) == true)
                {

                    // Step 1: Replace placeholders (if required)
                    string finalBody = mailTemplate;

                    if (isReplaceRequired && dictData != null)
                    {
                        finalBody = ReplaceTemplateData(mailTemplate, dictData);
                    }

                    // Step 2: Inject logo placeholder if needed
                    if (includeLogo)
                    {
                        if (logoBytes == null)
                            throw new Exception("Logo is enabled but logoBytes is null.");

                        // Replace {{Logo}} placeholder in template
                        finalBody = finalBody.Replace("{{Logo}}", $"<img src='cid:{logoContentId}' style='height:50px;' />");
                    }
                    else
                    {
                        // Remove logo placeholder if not needed
                        finalBody = finalBody.Replace("{{Logo}}", "");
                    }

                    // Step 3: Setup SMTP
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("naveendnavalay@gmail.com", ""),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("naveendnavalay@gmail.com"),
                        Subject = subject,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toMailID);

                    // Step 4: Create HTML view
                    var htmlView = AlternateView.CreateAlternateViewFromString(
                        finalBody,
                        Encoding.UTF8,
                        "text/html"
                    );

                    //// Step 5: Add logo (CID)
                    if (includeLogo && logoBytes != null)
                    {
                        var ms = new MemoryStream(logoBytes);

                        var logo = new LinkedResource(ms, "image/png")
                        {
                            ContentId = logoContentId,
                            TransferEncoding = System.Net.Mime.TransferEncoding.Base64
                        };

                        htmlView.LinkedResources.Add(logo);
                    }

                    mailMessage.AlternateViews.Add(htmlView);

                    // Step 6: Send mail                
                    smtpClient.Send(mailMessage);
                }
                //GeneratePdf(finalBody, @"C:\temp\Attendance.pdf");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while sending email: " + ex.Message);
            }
        }

        private string ReplaceTemplateData(string template, Dictionary<string, string> dictData)
        {
            foreach (var item in dictData)
            {
                string placeholder = "{{" + item.Key + "}}";

                template = ReplaceIgnoreCase(template, placeholder, item.Value ?? string.Empty);
            }

            return template;
        }

        private string ReplaceIgnoreCase(string input, string search, string replacement)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(search))
                return input;

            int prevIndex = 0;
            int index = input.IndexOf(search, StringComparison.OrdinalIgnoreCase);

            while (index >= 0)
            {
                input = input.Remove(index, search.Length)
                             .Insert(index, replacement);

                prevIndex = index + replacement.Length;
                index = input.IndexOf(search, prevIndex, StringComparison.OrdinalIgnoreCase);
            }

            return input;
        }

        public void GeneratePdf(string htmlContent, string outputPath)
        {
            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                                    ColorMode = ColorMode.Color,
                                    Orientation = Orientation.Portrait,
                                    PaperSize = DinkToPdf.PaperKind.A4
                                 },
                Objects = {
                                new ObjectSettings()
                                {
                                    HtmlContent = htmlContent,
                                    WebSettings = { DefaultEncoding = "utf-8" }
                                }
                            }
            };

            byte[] pdf = converter.Convert(doc);

            File.WriteAllBytes(outputPath, pdf);
        }
    }
}
