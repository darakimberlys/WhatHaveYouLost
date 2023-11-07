using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using WhatYouHaveLost.Controllers;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Tests.Controller;

public class HomeControllerTests
{
    private readonly Mock<INewsRepository> _newsRepositoryMock = new();
    private readonly Mock<IAuthenticationService> _authenticationServiceMock = new();
    private readonly Mock<INewsService> _newsServiceMock = new();
    private readonly Mock<IPasswordEncryptor> _passwordEncryptorMock = new();
    private readonly Mock<IJwtTokenValidator> _jwtTokenValidatorMock = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<HttpRequest> _httpRequest = new();

    private readonly HomeController _homeController;

    public HomeControllerTests()
    {
        Environment.SetEnvironmentVariable("JwtSecret", "1234545688885555");

        _homeController = new HomeController(
            _newsRepositoryMock.Object,
            _newsServiceMock.Object,
            _authenticationServiceMock.Object,
            _passwordEncryptorMock.Object,
            _jwtTokenValidatorMock.Object);
    }

    [Theory]
    [InlineData(null, null, false, "FailedResult")]
    [InlineData("", "", false, "FailedResult")]
    [InlineData("username", "password", true, "Success")]
    public async Task LoginPage_Post_InvalidModel_ReturnsView(
        string userName,
        string password,
        bool isValidToken,
        string testResult)
    {
        if (testResult == "Success")
        {
            _homeController.ControllerContext = new ControllerContext();
            _homeController.ControllerContext.HttpContext = new DefaultHttpContext();

            _homeController.Request.Headers["Authorization"] = $"Bearer {password}";
        }

        // Arrange
        var loginModel = new LoginModel
        {
            UserName = userName,
            Password = password
        };

        var userData = new UserData
        {
            LoginName = userName,
            Password = password
        };

        _authenticationServiceMock.Setup(x =>
                x.LoginAsync(It.IsAny<UserData>()))
            .ReturnsAsync((isValidToken, loginModel.Password));

        _passwordEncryptorMock.Setup(p =>
                p.EncryptPassword(It.IsAny<string>()))
            .Returns(loginModel.Password);

        _passwordEncryptorMock.Setup(p =>
                p.DecryptPassword(It.IsAny<string>()))
            .Returns(loginModel.Password);

        _userRepository.Setup(up =>
                up.GetUserDataAsync(userData.LoginName))
            .ReturnsAsync(userData);


        // Act
        var result = await _homeController.LoginPage(loginModel) as RedirectToActionResult;

        // Assert

        if (testResult == "Success")
        {
            Assert.NotNull(result);
            Assert.True(_homeController.ModelState.IsValid);
        }
        else
        {
            Assert.Null(result);
            Assert.False(_homeController.ModelState.IsValid);
        }
    }

    [Theory]
    [InlineData(null, false, "FailedResult")]
    [InlineData("", false, "FailedResult")]
    [InlineData("", false, "InvalidModelState")]
    [InlineData("password", true, "Success")]
    public async Task CreateNews_Post_InvalidModel_ReturnsView(
        string password,
        bool isValidToken,
        string testResult)
    {
        if (testResult == "Success")
        {
            _homeController.ControllerContext = new ControllerContext();
            _homeController.ControllerContext.HttpContext = new DefaultHttpContext();

            _homeController.Request.Headers["Authorization"] = $"Bearer {password}";
        }

        // Arrange

        var createNewsModel = new CreateNewsModel()
        {
            Title = "TitleTest",
            Content = "ContentLink"
        };


        var userData = new UserData
        {
            Password = password
        };

        // _authenticationServiceMock.Setup(x =>
        //         x.LoginAsync(It.IsAny<UserData>()))
        //     .ReturnsAsync((isValidToken, userData.Password));

        // _userRepository.Setup(up =>
        //         up.GetUserDataAsync(loginModel.UserName))
        //     .ReturnsAsync(userData);

        _jwtTokenValidatorMock.Setup(x =>
                x.ValidateToken(It.IsAny<string>()))
            .Returns(isValidToken);

        _passwordEncryptorMock.Setup(p =>
                p.EncryptPassword(It.IsAny<string>()))
            .Returns(userData.Password);

        _passwordEncryptorMock.Setup(p =>
                p.DecryptPassword(It.IsAny<string>()))
            .Returns(userData.Password);

        _userRepository.Setup(up =>
                up.GetUserDataAsync(userData.LoginName))
            .ReturnsAsync(userData);


        // Act
        if (testResult == "InvalidModelState")
        {
            _homeController.ModelState.AddModelError("Propriedade", "InvalidModelState");
        }

        var result = await _homeController.CreateNews(null) as RedirectToActionResult;

        // Assert

        if (testResult == "Success")
        {
            Assert.True(_homeController.ModelState.IsValid);
        }
        else
        {
            Assert.False(_homeController.ModelState.IsValid);
        }

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(1, false, "FailedResult")]
    [InlineData(0, false, "FailedResult")]
    [InlineData(0, false, "InvalidModelState")]
    [InlineData(1, true, "Success")]
    public async Task UpdateNews_Validate(
        int idNews,
        bool isValidToken,
        string testResult)
    {
        // Arrange
        var upsertModel = new UpsertModel
        {
            Title = "TitleTest",
            Content = "ContentTest"
        };

        var news = new News
        {
            Title = "TitleTest",
            Content = "ContentTest",
            Author = "AuthorTest",
            Id = 1,
            Image = "Test",
            PublishDate = new DateTime().Date
        };

        _newsRepositoryMock.Setup(up =>
                up.GetCompleteNewsByIdAsync(idNews))
            .ReturnsAsync(news);      
        
        _jwtTokenValidatorMock.Setup(x =>
                x.ValidateToken(It.IsAny<string>()))
            .Returns(isValidToken);

        _newsServiceMock.Setup(up =>
                up.UpdateNewsAsync(It.IsAny<UpsertModel>()))
            .ReturnsAsync(true);




        // Act
        if (testResult == "InvalidModelState")
        {
            _homeController.ModelState.AddModelError("Propriedade", "InvalidModelState");
        }

        var result = await _homeController.UpdateNews(idNews);

        // Assert

        if (testResult == "InvalidModelState")
        {
            Assert.False(_homeController.ModelState.IsValid);
        }
        else
        {
            Assert.True(_homeController.ModelState.IsValid);
        }

        Assert.NotNull(result);
    }
}