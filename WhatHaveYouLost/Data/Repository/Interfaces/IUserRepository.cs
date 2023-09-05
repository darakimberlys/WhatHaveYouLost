namespace WhatYouHaveLost.Data.Repository.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistAsync(string userName);
}