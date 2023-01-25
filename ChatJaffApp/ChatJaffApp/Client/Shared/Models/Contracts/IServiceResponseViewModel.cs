namespace ChatJaffApp.Client.Shared.Models.Contracts
{
    public interface IServiceResponseViewModel<T>
    {
        T? Data { get; set; }
        bool Success { get; set; }
        string? Message { get; set; }
    }
}
