using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncryptor _passwordEncryptor;
    private readonly IAuthCacheService _authCacheService;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordEncryptor passwordEncryptor,
        IAuthCacheService authCacheService,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
        _authCacheService = authCacheService;
    }

    public async Task<bool> LoginAsync(UserData userData)
    {
        var user = await _userRepository.UserExistAsync(userData.LoginName);

        if (user is false)
        {
            return false;
        }

        var userPassword = _passwordEncryptor.DecryptPassword(userData.Password);

        if (!string.IsNullOrWhiteSpace(userPassword) && !string.IsNullOrWhiteSpace(userData.Password))
        {
            if (userData.Password == userPassword)
            {
                var token = GenerateJwtToken(userData);
                await _authCacheService.SetTokenCacheAsync(Guid.Parse(userData.UserId.ToString()), token);
                return true;
            }

            return false;
        }

        return false;
    }

    private string GenerateJwtToken(UserData userData)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSecret").Value);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userData.LoginName),
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

