using System.Security.Cryptography;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Services.Authentication;

public class RijndaelProvider : IRijndaelProvider
{
    public ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
    {
        using (RijndaelManaged aesAlg = new RijndaelManaged())
        {
            return aesAlg.CreateDecryptor(key, iv);
        }
    }

    public ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
    {
        using (RijndaelManaged aesAlg = new RijndaelManaged())
        {
            return aesAlg.CreateEncryptor(key, iv);
        }
    }
    
    public RijndaelManaged CreateInstance()
    {
        return new RijndaelManaged();
    }
}