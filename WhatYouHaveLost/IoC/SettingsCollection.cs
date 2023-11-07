using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WhatYouHaveLost.Data.Repository;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.IoC;

public static class SettingsCollection
{
    public static void AddJWTValidation(this IServiceCollection services, IConfiguration configuration)
    {
        var secret = Environment.GetEnvironmentVariable("JwtSecret");
        var key = Encoding.ASCII.GetBytes(secret);
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddScoped<IPasswordEncryptor>(provider =>
        {
            return new PasswordEncryptor(Environment.GetEnvironmentVariable("JwtSecret"));
        });   
        
        services.AddScoped<IJwtTokenValidator>(_ =>
        {
            var encryptionKey = Environment.GetEnvironmentVariable("JwtSecret");
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
    }
}