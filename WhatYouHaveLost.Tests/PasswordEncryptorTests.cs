using System.Security.Cryptography;
using System.Text;
using AutoFixture;
using Moq;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Tests;

public class PasswordEncryptorTests
{
    private const string EncryptionKey = "1234545688885555";

    private readonly Fixture _fixture;
    private readonly Mock<RijndaelManaged> _rijndaelManaged = new();
    private readonly Mock<IRijndaelProvider> _rijndaelProvider = new();
    private readonly byte[] _encryptionKeyByte = Encoding.UTF8.GetBytes(EncryptionKey);
    private readonly byte[] _ivBytes = new byte[16];
    private readonly IPasswordEncryptor _passwordEncryptor;

    public PasswordEncryptorTests()
    {
        Environment.SetEnvironmentVariable("JwtSecret", "1234545688885555");
       
        _passwordEncryptor  = new PasswordEncryptor(EncryptionKey, _rijndaelProvider.Object);
    }
    
    [Theory]
    [InlineData("passwordTest", "passwordTest", "Success")]
    [InlineData("passwordTest", null, "SecondNull")]
    [InlineData("passwordTest", "", "SecondEmpty")]
    [InlineData(null, "passwordTest", "FirstNull")]
    [InlineData("", "passwordTest", "FirstEmpty")]
    public void ValidateEncryptor(
        string password, 
        string secondPassword, 
        string testType)
    {
        var decrypt = _fixture.Create<ICryptoTransform>();
        
        var rijndaelProviderMock = new Mock<IRijndaelProvider>();

        rijndaelProviderMock.Setup(r => 
            r.CreateInstance()).Returns(new RijndaelManaged());
        
        var encryptResult = _passwordEncryptor.EncryptPassword(password);
        var decryptResult = _passwordEncryptor.DecryptPassword(secondPassword);
       
       switch (testType)
       {
           case "Success":
               Assert.NotEmpty(encryptResult);
               Assert.NotEmpty(decryptResult);
               break;

           case "SecondNull":

               Assert.NotEmpty(encryptResult);
               Assert.Empty(decryptResult);
               break;

           case "SecondEmpty":
               Assert.NotEmpty(encryptResult);
               Assert.Empty(decryptResult);
               break;

           case "FirstNull":
               Assert.Empty(encryptResult);
               Assert.NotEmpty(decryptResult);
               break;

           case "FirstEmpty":
               
               _rijndaelManaged.Setup(x =>
                       x.CreateDecryptor(It.IsAny<byte[]>(), It.IsAny<byte[]?>()))
                   .Returns(decrypt);

               Assert.Empty(encryptResult);
               Assert.NotEmpty(decryptResult);
               break;

           default:
               throw new ArgumentException($"Invalid testType: {testType}");
       }
    }
}