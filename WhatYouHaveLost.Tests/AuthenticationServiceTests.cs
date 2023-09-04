using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services;

namespace WhatYouHaveLost.Tests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IServiceProvider> _serviceProvider;
        private readonly Mock<UserManager<UserData>> _userManagerMock;
        
        public AuthenticationServiceTests()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _userManagerMock = new Mock<UserManager<UserData>>();
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<UserData>>(
                Mock.Of<IUserStore<UserData>>(), null, null, null, null, null, null, null, null);
            var signInManagerMock = new Mock<SignInManager<UserData>>(
                userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserData>>(), null, null, null, null);
            var userRepositoryMock = new Mock<IUserRepository>();

            var authService = new AuthenticationService(
                userManagerMock.Object,
                signInManagerMock.Object);

            var username = "testuser";
            var passwordString = "testpassword";
            var password = BinaryData.FromString(passwordString);

            userManagerMock.Setup(m => m.FindByNameAsync(username))
                .ReturnsAsync(new UserData { LoginName = username });

            signInManagerMock.Setup(m => m.PasswordSignInAsync(It.IsAny<UserData>(), passwordString, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var loginResult = await authService.LoginAsync(username, password);

            // Assert
            Assert.Equal(Microsoft.AspNetCore.Identity.SignInResult.Success, loginResult);
        }

        // [Fact]
        // public async Task LoginAsync_InvalidCredentials_ReturnsFailed()
        // {
        //     // Arrange
        //     var authService = _serviceProvider.GetRequiredService<IAuthenticationService>();
        //     var username = "testuser";
        //     var passwordString = "testpassword";
        //     var password = BinaryData.FromString(passwordString);
        //
        //     // Act
        //     var loginResult = await authService.LoginAsync(username, password);
        //
        //     // Assert
        //     Assert.True(loginResult.IsNotAllowed); // You can change this assertion based on your actual logic
        // }

        // Add more test cases as needed
    }
}
