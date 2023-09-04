using Microsoft.AspNetCore.Identity;

namespace WhatYouHaveLost.Services.Interfaces;

public interface IAuthenticationService
{
    Task<SignInResult> LoginAsync(string username, BinaryData password);
    Task SignOutAsync();
}