namespace WhatYouHaveLost.Services.Interfaces;

public interface IAuthCacheService
{
    Task SetTokenCacheAsync(Guid userId, string token);
    Task<string> GetTokenCacheAsync(Guid userId);
}