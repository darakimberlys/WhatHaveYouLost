using System.Text;
using AutoFixture;
using WhatYouHaveLost.Data.Repository.Configurations;

namespace WhatYouHaveLost.Tests;

public class PasswordEncryptorTests
{
    private const string EncryptionKey = "1234545688885555";

    private readonly Fixture _fixture;
    private readonly byte[] _encryptionKeyByte = Encoding.UTF8.GetBytes(EncryptionKey);
    private readonly byte[] _ivBytes = new byte[16];
    private readonly IPasswordEncryptor _passwordEncryptor;

    public PasswordEncryptorTests()
    {
        Environment.SetEnvironmentVariable("JwtSecret", "1234545688885555");

        _passwordEncryptor = new PasswordEncryptor(EncryptionKey);
    }

    [Theory]
    [InlineData("passwordTest", "passwordTest", "Success")]
    [InlineData("passwordTest", null, "SecondNull")]
    [InlineData("passwordTest", "", "SecondEmpty")]
    [InlineData(null, "passwordTest", "FirstNull")]
    [InlineData("", "passwordTest", "FirstEmpty")]
    public void ValidateEncryptor(
        string passwordToEncrypt,
        string passwordToDecrypt,
        string testType)
    {
        var encryptResult = "";
        var decryptResult = "";
        var exception = new ArgumentNullException();

        switch (testType)
        {
            case "Success":
                encryptResult = _passwordEncryptor.EncryptPassword(passwordToEncrypt);
                decryptResult = _passwordEncryptor.DecryptPassword(encryptResult);

                Assert.NotEmpty(encryptResult);
                Assert.NotEmpty(decryptResult);
                break;

            case "SecondNull" or "SecondEmpty":
                encryptResult = _passwordEncryptor.EncryptPassword(passwordToEncrypt);
                
                exception = Assert.ThrowsAny<ArgumentNullException>(() =>
                    _passwordEncryptor.DecryptPassword(passwordToDecrypt));

                Assert.Equal("encryptedPassword", exception.ParamName);
                Assert.NotEmpty(encryptResult);
                break;

            case "FirstNull" or "FirstEmpty":
                exception = Assert.ThrowsAny<ArgumentNullException>(() => _passwordEncryptor.EncryptPassword(passwordToEncrypt));

                Assert.Equal("password", exception.ParamName);
                break;

            default:
                throw new ArgumentException($"Invalid testType: {testType}");
        }
    }
}