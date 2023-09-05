using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository.Interfaces;

public interface IUserRepository
{
    Task<UserData> GetUserDataAsync(string userName);
}