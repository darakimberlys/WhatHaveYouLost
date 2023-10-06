using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Tests;

public class AuthenticationServiceTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IPasswordEncryptor> _passwordEncryptor;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly IAuthenticationService _authenticationService;
        
    public AuthenticationServiceTests()
    {
        Environment.SetEnvironmentVariable("JwtSecret", "1234545688885555");
        
        _userRepository = new Mock<IUserRepository>();
        _passwordEncryptor = new Mock<IPasswordEncryptor>();
        _configurationMock = new Mock<IConfiguration>();
        _authenticationService = new AuthenticationService(
            _userRepository.Object,
            _passwordEncryptor.Object,
            _configurationMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
    {
        var userData = new UserData
        {
            LoginName = "testuser",
            Password = "testpassword"
        };
        
        _passwordEncryptor.Setup(p =>
                p.EncryptPassword(It.IsAny<string>()))
            .Returns(userData.Password);
        
        _passwordEncryptor.Setup(p =>
                p.DecryptPassword(It.IsAny<string>()))
            .Returns(userData.Password);
        
        _userRepository.Setup(up =>
                up.GetUserDataAsync(userData.LoginName))
            .ReturnsAsync(userData);

        var loginResult = await _authenticationService.LoginAsync(userData);

        Assert.True(loginResult.Item1);
    }
    
    [Fact]
    public async Task LoginAsync_InvalidCredentials_ReturnsFailed()
    {
        // Arrange
        //var authService = _serviceProvider.<IAuthenticationService>();
        
        var userData = new UserData
        {
            LoginName = "testuser",
            Password = "testpassword"
        };
        
        var password = BinaryData.FromString(userData.Password);
        
        // Act
        var loginResult = await _authenticationService.LoginAsync(userData);
        
        // Assert
        //Assert.True(loginResult.IsNotAllowed); // You can change this assertion based on your actual logic
    }
}