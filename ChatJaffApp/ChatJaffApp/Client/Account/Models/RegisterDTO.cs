namespace ChatJaffApp.Client.Account.Models
{
    public class RegisterDTO
    {
        public string? Email { get; set; }

        public string? Username { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }

        public bool? AgreedToUserTerms { get; set; } = false;


    }
}
