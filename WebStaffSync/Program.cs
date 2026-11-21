using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using WebStaffSync.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();
builder.Services.AddHttpClient("IntegrationService", client =>
{
    var baseUrl = builder.Configuration["IntegrationService:BaseUrl"];
    if (string.IsNullOrWhiteSpace(baseUrl))
        throw new InvalidOperationException("IntegrationService:BaseUrl is missing from configuration.");

    client.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddDataProtection()
    .SetApplicationName("StaffSync.Web")
    .PersistKeysToFileSystem(
        new DirectoryInfo(
            @"C:\StaffSyncDataProtectionKeys"));

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/Index";
        options.Cookie.Name = "WebStaffSync.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Reject a tenant slug that does not match this WebStaffSync deployment.
// The URL segment is for routing only; it never selects a database or employee.
app.Use(async (context, next) =>
{
    if (context.Request.RouteValues.TryGetValue("client", out var routeValue) && routeValue != null)
    {
        var configuredSlug = TenantRouteHelper.GetConfiguredSlug(app.Configuration);
        var requestedSlug = Convert.ToString(routeValue);

        if (string.IsNullOrWhiteSpace(configuredSlug))
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Tenant:ClientSlug is not configured.");
            return;
        }

        if (!string.Equals(requestedSlug, configuredSlug, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Canonical employee self-service URLs.
app.MapControllerRoute(
    name: "tenant-home",
    pattern: "{client}/home",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "tenant-myprofile",
    pattern: "{client}/myprofile",
    defaults: new { controller = "MyProfile", action = "Index" });

// Keep other controller actions available under the tenant prefix.
app.MapControllerRoute(
    name: "tenant-default",
    pattern: "{client}/{controller}/{action}/{id?}");

// Login remains available at /Login/Index. This also preserves compatibility
// with any existing non-tenant URLs during rollout.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
