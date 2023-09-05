using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Services.Interfaces;

public interface IAuthenticationService
{
    Task<bool> LoginAsync(UserData userData);
}