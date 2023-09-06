using System.Security.Cryptography;
using System.Text;

namespace WhatYouHaveLost.Data.Repository.Configurations
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        private readonly byte[] _encryptionKeyBytes;
        private readonly byte[] _ivBytes;

        public PasswordEncryptor(string encryptionKey)
        {
            _encryptionKeyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            _ivBytes = new byte[16];
        }

        public string EncryptPassword(string password)
        {
            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = _encryptionKeyBytes;
                aesAlg.IV = _ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

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
                aesAlg.Key = _encryptionKeyBytes;
                aesAlg.IV = _ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

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
}
