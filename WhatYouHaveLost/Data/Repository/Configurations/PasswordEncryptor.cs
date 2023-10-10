using System.Security.Cryptography;
using System.Text;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Data.Repository.Configurations;

public class PasswordEncryptor : IPasswordEncryptor
{
    private readonly byte[] _encryptionKeyBytes;
    private readonly byte[] _ivBytes = new byte[16];
    private readonly IRijndaelProvider _rijndaelProvider;

    public PasswordEncryptor(string encryptionKey, IRijndaelProvider rijndaelProvider)
    {
        _encryptionKeyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        _rijndaelProvider = rijndaelProvider;
    }

    public string EncryptPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return string.Empty;
        }

        var encryptor = _rijndaelProvider.CreateEncryptor(_encryptionKeyBytes, _ivBytes);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(password);
                }
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }
    }

    public string DecryptPassword(string encryptedPassword)
    {
        if (string.IsNullOrWhiteSpace(encryptedPassword))
        {
            return string.Empty;
        }

        var decryptor = _rijndaelProvider.CreateDecryptor(_encryptionKeyBytes, _ivBytes);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}