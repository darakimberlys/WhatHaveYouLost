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
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordEncryptor passwordEncryptor,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task<(bool, string)> LoginAsync(UserData userData)
    {
        var user = await _userRepository.GetUserDataAsync(userData.LoginName);

        if (user is null)
        {
            return (false, string.Empty);
        }

        var userPassword = _passwordEncryptor.DecryptPassword(userData.Password);

        if (user.Password == userPassword)
        {
           var token =  GenerateJwtToken(userData);
            return (true, token);
        }
        else
        {
            return (false, string.Empty);
        }
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
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };


        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}