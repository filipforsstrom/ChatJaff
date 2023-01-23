namespace ChatJaffApp.Client.Account.Contracts
{
    public interface IRegisterResponse
    {
        string Data { get; set; }
        bool Success { get; set; }
    }
}
