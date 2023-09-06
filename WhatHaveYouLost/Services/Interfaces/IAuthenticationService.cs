using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Services.Interfaces;

public interface IAuthenticationService
{
    Task<(bool, string)> LoginAsync(UserData userData);
}