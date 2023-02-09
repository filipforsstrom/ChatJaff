using ChatJaffApp.Client.ChatRoom.Messages.Contracts;
using ChatJaffApp.Client.Shared.Models;
using ChatJaffApp.Client.Shared.Models.Contracts;

namespace ChatJaffApp.Client.ChatRoom.Messages.Services
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task GetChatMessages()
        {
            throw new NotImplementedException();
        }

        public Task SendMessage()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseViewModel<string>> DeleteMessage(Guid id)
        {
            ServiceResponseViewModel<string> responseViewModel = new();

            var response = await _httpClient.DeleteAsync($"/api/message/{id}");

            if(!response.IsSuccessStatusCode){
                responseViewModel.Message = "Something went wrong";
                responseViewModel.Success = false;
            }

            responseViewModel.Message = "Message deleted";
            responseViewModel.Success = true;
            return responseViewModel;
        }
    }
}
