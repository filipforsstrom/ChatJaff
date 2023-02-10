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



    }
}
