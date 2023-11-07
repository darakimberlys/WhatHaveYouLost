using Microsoft.Extensions.Logging;
using Moq;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Tests;

public class AuthenticationServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IPasswordEncryptor> _passwordEncryptor = new();
    private readonly Mock<ILogger<AuthenticationService>> _loggerMock = new();
    private readonly IAuthenticationService _authenticationService;
        
    public AuthenticationServiceTests()
    {
        Environment.SetEnvironmentVariable("JwtSecret", "1234545688885555");
        
        _authenticationService = new AuthenticationService(
            _userRepository.Object,
            _passwordEncryptor.Object,
            _loggerMock.Object);
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
        var userData = new UserData
        {
            LoginName = "testuser",
            Password = "testpassword"
        };
        
        _passwordEncryptor.Setup(p =>
                p.EncryptPassword(It.IsAny<string>()))
            .Returns("expired");
        
        _passwordEncryptor.Setup(p =>
                p.DecryptPassword(It.IsAny<string>()))
            .Returns("expired");
        
        _userRepository.Setup(up =>
                up.GetUserDataAsync(userData.LoginName))
            .ReturnsAsync(userData);

        var loginResult = await _authenticationService.LoginAsync(userData);

        Assert.False(loginResult.Item1);
    }
    
    [Theory]
    [InlineData("testUser", "testPassword", true)]
    [InlineData(null, null, false)]
    [InlineData("", "", false)]
    [InlineData(null, "testPassword", false)]
    [InlineData("", "testPassword", false)]
    [InlineData("testUser", null, false)]
    [InlineData("testUser", "", false)]
    public async Task LoginAsync_InvalidCredentials_ReturnsEmpty(
        string loginName, string password, bool result)
    {
        var userData = new UserData
        {
            LoginName = loginName,
            Password = password
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

        Assert.Equal(result, loginResult.Item1);
    }
}