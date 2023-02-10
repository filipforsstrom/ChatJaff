using System.Text;

namespace ChatJaffApp.Server.ChatRoom.Encryption
{
    public class EncryptionHelper
    {
        public string RandomKey()
        {
            Random res = new Random();
            String str = "abcdefghijklmnopqrstuvwxyz0123456789!#¤%&/=@£€€{[]{";
            int size = 15;
            String randomstring = "";

            for (int i = 0; i < size; i++)
            {   
                int x = res.Next(str.Length);
                randomstring = randomstring + str[x];
            }
            return randomstring;
        }

        public string GenerateDbKey() 
        {
            var firstPart=RandomKey();
            var secondPart=RandomKey();
            var key = $"{firstPart}.{secondPart}";
            return key;

        
        }

        public string EncryptMessage(string message, string chatKey)
        {

            var keyStringArray = chatKey.Split(".");
            string key = keyStringArray[0];

            // skapa salt
            byte[] salt = Encoding.Unicode.GetBytes(keyStringArray[1]);

            return AesEncryptManager.Encrypt(message, key, salt);
        }



    }
}
