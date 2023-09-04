using Microsoft.AspNetCore.Identity;

namespace WhatYouHaveLost.IoC;

public static class SettingsCollection
{

    public static void AddIdentityValidation(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.User.RequireUniqueEmail = true;        
        });
        
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.LoginPath = "/Home/Login"; 
            options.AccessDeniedPath = "/Home/Account/AccessDenied"; // Define a página de acesso negado personalizada
            options.SlidingExpiration = true;
        });

    }
}