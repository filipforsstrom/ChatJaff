using ChatJaffApp.Server.ChatRoom.Controllers;
using ChatJaffApp.Server.ChatRoom.Encryption;
using ChatJaffApp.Server.ChatRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace ChatJaffApi.Tests.UnitTests
{
    public class EncryptionManagerTest
    {
        private string key { get; set; }
        private byte[] salt { get; set; }
        private string message { get; set; }
        private string generatedKey { get; set; }
        private byte[] generatedSalt { get; set; }

        public EncryptionManagerTest()
        {
            key = "master key";
            salt = Encoding.Unicode.GetBytes("banana");
            message = "secret message";
           
            EncryptionHelper encryptionHelper = new();
            var generatedKeyAndSalt=encryptionHelper.GenerateDbKey();

            string[] keyList = generatedKeyAndSalt.Split(".");
            generatedKey= keyList[0];
            generatedSalt= Encoding.Unicode.GetBytes(keyList[1]) ;

        }

        [Fact]
        public void EncryptMessage_OnSuccess_ReturnsDifferentString()
        {            

            var encryptedMsg = AesEncryptManager.Encrypt(message, key, salt);

            Assert.NotEqual(message, encryptedMsg);
        }

        [Fact]
        public void DecryptMessage_WithCorrectParams_ReturnsSameString()
        {            
            var encryptedMsg = AesEncryptManager.Encrypt(message, key, salt);
            
            var decryptedMessage = AesEncryptManager.Decrypt(encryptedMsg, key, salt);

            
            Assert.Equal(decryptedMessage, message);
        }

        [Fact]
        public void DecryptMessage_WithWrongKey_ThrowsException()
        {
            var originalKey = key;
            var wrongKey = "wrong key";

            var encryptedMsg = AesEncryptManager.Encrypt(message, originalKey, salt);

            Func<string> act = () => AesEncryptManager.Decrypt(encryptedMsg, wrongKey, salt);

            Assert.Throws<CryptographicException>(act);
        }

        [Fact]
        public void DecryptMessage_WithWrongSalt_ThrowsException()
        {            
            var wrongSalt = Encoding.Unicode.GetBytes("oranges");

            var encryptedMsg = AesEncryptManager.Encrypt(message, key, salt);

            Func<string> act = () => AesEncryptManager.Decrypt(encryptedMsg, key, wrongSalt);

            Assert.Throws<CryptographicException>(act);
        }

        [Fact]
        public void EncryptMessage_WithGeneratedKey_OnSuccess_ReturnsDifferentString()
        {
            

            var encryptedMsg = AesEncryptManager.Encrypt(message, generatedKey, generatedSalt);

            Assert.NotEqual(message, encryptedMsg);
        }

        [Fact]
        public void EncryptMessage_WithGeneratedKey_ReturnSameString()
        {

            var encryptedMsg = AesEncryptManager.Encrypt(message, generatedKey, generatedSalt);

            var decryptedMessage = AesEncryptManager.Decrypt(encryptedMsg, generatedKey, generatedSalt);

            Assert.Equal(message, decryptedMessage);
        }
    }
}
