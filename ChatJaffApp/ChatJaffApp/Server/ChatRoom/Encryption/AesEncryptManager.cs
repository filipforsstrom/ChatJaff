using System.Security.Cryptography;
using System.Text;

namespace ChatJaffApp.Server.ChatRoom.Encryption
{
    public static class AesEncryptManager
    {
        private static readonly int iterations = 2000;

        public static string Encrypt(
            string plainText, string key, byte[] salt)
        {

            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

            Aes aes = Aes.Create();

            var pbkdf2 = new Rfc2898DeriveBytes(key, salt, iterations, HashAlgorithmName.SHA256);

            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes,0, plainBytes.Length);
                }
                encryptedBytes= ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cryptoText, string key, byte[] salt)
        {
            
            byte[] plainBytes;
            byte[] cryptoBytes = Convert
            .FromBase64String(cryptoText);

            Aes aes = Aes.Create();

            var pbkdf2 = new Rfc2898DeriveBytes(key, salt, iterations, HashAlgorithmName.SHA256);

            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }

        public static string CreateDbKey(string key, string iv)
        {
            return string.Empty;
        }
    }
}
