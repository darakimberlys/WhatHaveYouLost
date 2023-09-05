using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WhatYouHaveLost.Data.Repository;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Infrastructure;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.IoC;

public static class SettingsCollection
{
    public static void AddJWTValidation(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSecret").Value);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
        
        services.AddScoped<ICacheProvider, CacheProvider>();
        
        services.AddScoped<IPasswordEncryptor>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var encryptionKey = configuration.GetValue<string>("JwtSecret");
            return new PasswordEncryptor(encryptionKey);
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
        services.AddScoped<IAuthCacheService, AuthCacheService>();

    }

    public static void AddDataBaseConnection(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION"));
        });
    }
}