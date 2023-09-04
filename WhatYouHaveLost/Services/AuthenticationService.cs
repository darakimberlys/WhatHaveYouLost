using Microsoft.AspNetCore.Identity;
using WhatYouHaveLost.Data.Repository;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<UserData> _userManager;
    private readonly SignInManager<UserData> _signInManager;

    public AuthenticationService(
        UserManager<UserData> userManager,
        SignInManager<UserData> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LoginAsync(string username, BinaryData password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user != null)
        {
            return await _signInManager.PasswordSignInAsync(user, password.ToString(), isPersistent: false, lockoutOnFailure: false);
        }

        return SignInResult.Failed;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}

