using System.Diagnostics.CodeAnalysis;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interface;

namespace WhatYouHaveLost;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddScoped<INewsRepository, NewsRepository>();
        builder.Services.AddScoped<INewsService, NewsService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        
        app.Run();
    }
}