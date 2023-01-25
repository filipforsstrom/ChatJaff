using ChatJaffApp.Client.Shared.Models.Contracts;

namespace ChatJaffApp.Client.Shared.Models
{
    public class ServiceResponseViewModel<T> : IServiceResponseViewModel<T>
    {
        public T? Data { get ; set ; }
        public bool Success { get ; set ; }
        public string? Message { get ; set ; }
    }
}
