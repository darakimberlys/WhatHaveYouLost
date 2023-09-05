using System.Security.Cryptography;
using System.Text;

namespace WhatYouHaveLost.Data.Repository.Configurations;

public class PasswordEncryptor : IPasswordEncryptor
{
    private readonly string _encryptionKey;

    public PasswordEncryptor(string encryptionKey)
    {
        _encryptionKey = encryptionKey;
    }

    public string EncryptPassword(string password)
    {
        using (RijndaelManaged aesAlg = new RijndaelManaged())
        {
            aesAlg.KeySize = 128;
            aesAlg.BlockSize = 128;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            byte[] keyBytes = Encoding.UTF8.GetBytes(_encryptionKey);
            byte[] ivBytes = new byte[16]; // O IV (Vetor de Inicialização) deve ser aleatório e não secreto.

            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

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
    }

    public string DecryptPassword(string encryptedPassword)
    {
        using (RijndaelManaged aesAlg = new RijndaelManaged())
        {
            aesAlg.KeySize = 128;
            aesAlg.BlockSize = 128;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            byte[] keyBytes = Encoding.UTF8.GetBytes(_encryptionKey);
            byte[] ivBytes = new byte[16]; // O IV (Vetor de Inicialização) deve ser o mesmo usado para criptografar.

            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
