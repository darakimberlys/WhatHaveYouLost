using Microsoft.Extensions.Logging.ApplicationInsights;
using WhatYouHaveLost.IoC;

namespace WhatYouHaveLost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Host.ConfigureLogging(logging =>
        {
            logging.AddApplicationInsights("your_instrumentation_key");
            logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
        });
        
        builder.Services.AddRazorPages();
        builder.Services.AddServices();
        builder.Services.AddJWTValidation(builder);
        builder.Services.AddDataBaseConnection();
        builder.Services.AddHttpContextAccessor(); 

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.Run();
    }
}