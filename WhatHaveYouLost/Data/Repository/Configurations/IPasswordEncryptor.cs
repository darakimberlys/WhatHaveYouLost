namespace WhatYouHaveLost.Data.Repository.Configurations;

public interface IPasswordEncryptor
{
    string EncryptPassword(string password);
    string DecryptPassword(string encryptedPassword);
}