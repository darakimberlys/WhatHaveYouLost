using System.Security.Cryptography;

namespace WhatYouHaveLost.Services.Interfaces;

public interface IRijndaelProvider
{
    ICryptoTransform CreateDecryptor(byte[] key, byte[] iv);
    ICryptoTransform CreateEncryptor(byte[] key, byte[] iv);
    RijndaelManaged CreateInstance();
}