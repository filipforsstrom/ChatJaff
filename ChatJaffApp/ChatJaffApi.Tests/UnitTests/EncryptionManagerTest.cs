using ChatJaffApp.Server.ChatRoom.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJaffApi.Tests.UnitTests
{
    public class EncryptionManagerTest
    {
        [Fact]
        public void EncryptMessage_OnSuccess_ReturnsDifferentString()
        {
            var key = "master key";
            var salt = Encoding.Unicode.GetBytes("banana");
            var message = "secret message";

            var encryptedMsg = AesEncryptManager.Encrypt(message, key, salt);

            Assert.NotEqual(message, encryptedMsg);
        }

        [Fact]
        public void DecryptMessage_WithCorrectParams_ReturnsSameString()
        {
            var key = "master key";
            var salt = Encoding.Unicode.GetBytes("banana");
            var message = "secret message";

            var encryptedMsg = AesEncryptManager.Encrypt(message, key, salt);
            
            var decryptedMessage = AesEncryptManager.Decrypt(encryptedMsg, key, salt);

            
            Assert.Equal(decryptedMessage, message);
        }
    }
}
