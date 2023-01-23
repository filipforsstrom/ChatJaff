using ChatJaffApp.Client.Account.Contracts;

namespace ChatJaffApp.Client.Account.Models
{
    public class RegisterResponse : IRegisterResponse
    {
        public string Data { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
