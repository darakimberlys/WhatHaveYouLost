using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Serilog.Context;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordEncryptor passwordEncryptor,
        ILogger<AuthenticationService> logger)

    {
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
        _logger = logger;
    }

    public async Task<(bool, string)> LoginAsync(UserData userData)
    {
        var user = await _userRepository.GetUserDataAsync(userData.LoginName);

        if (user is null)
        {
            using (LogContext.PushProperty("user", userData.LoginName))
            {
                _logger.LogError("User not found");
            }

            return (false, string.Empty);
        }

        var userPassword = _passwordEncryptor.DecryptPassword(userData.Password);
        
        if (user.Password != userPassword || string.IsNullOrWhiteSpace(userPassword))
        {
            using (LogContext.PushProperty("user", user.LoginName))
            {
                _logger.LogError("Invalid password");
            }
            return (false, string.Empty);
        }

        _logger.LogInformation("Success to login!");
        var token = GenerateJwtToken(userData);
        return (true, token);
    }

    private static string GenerateJwtToken(UserData userData)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret"));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userData.LoginName),
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}