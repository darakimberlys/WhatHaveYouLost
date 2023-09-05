namespace WhatYouHaveLost.Data.Repository.Configurations;

public interface IJwtTokenValidator
{
    bool ValidateToken(string token);
}