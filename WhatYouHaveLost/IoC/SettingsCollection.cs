using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WhatYouHaveLost.Data.Repository;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.IoC;

public static class SettingsCollection
{
    public static void AddJWTValidation(this IServiceCollection services, WebApplicationBuilder? builder)
    {
        var secret = Environment.GetEnvironmentVariable("JwtSecret");
        var key = Encoding.ASCII.GetBytes(secret);
        
        //AddIdentity
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
        
        //AddAuthentication
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //AddJwtBearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        
        services.AddScoped<IPasswordEncryptor>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var encryptionKey = configuration.GetValue<string>("JwtSecret");
            return new PasswordEncryptor(encryptionKey);
        });   
        
        services.AddScoped<IJwtTokenValidator>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var encryptionKey = configuration.GetValue<string>("JwtSecret");
            return new JwtTokenValidator(encryptionKey);
        });
    }

    public static void AddServices(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        //Services
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    public static void AddDataBaseConnection(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION"));
        });        
        
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION"));
        });
        
        services.AddIdentity<UserData, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

    }
}