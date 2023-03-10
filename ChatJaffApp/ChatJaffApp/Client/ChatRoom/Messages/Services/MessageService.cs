using ChatJaffApp.Client.ChatRoom.Messages.Contracts;
using ChatJaffApp.Client.ChatRoom.Messages.Models;
using ChatJaffApp.Client.Shared.Models;
using ChatJaffApp.Client.Shared.Models.Contracts;
using System.Net.Http.Json;
using static ChatJaffApp.Client.ChatRoom.Pages.ChatRoom;

namespace ChatJaffApp.Client.ChatRoom.Messages.Services
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponseViewModel<string>> EditMessage(EditMessageRequest newMessage)
        {
            ServiceResponseViewModel<string> responseViewModel = new();

            var response = await _httpClient.PutAsJsonAsync($"/api/message/{newMessage.MessageId}", newMessage);
            if(!response.IsSuccessStatusCode)
            {
                responseViewModel.Success = false;
                responseViewModel.Message = response.Content.ToString();

            }
            responseViewModel.Success = true;
            responseViewModel.Message = "";
            responseViewModel.Data = await response.Content.ReadAsStringAsync();

            return responseViewModel;
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
        public async Task<ServiceResponseViewModel<string>> FlagMessage(Guid messageId)
        {
            ServiceResponseViewModel<string> responseViewModel = new();
            var response = await _httpClient.PutAsJsonAsync($"/api/message/", messageId );

            if (!response.IsSuccessStatusCode)
            {
                responseViewModel.Message = "Something went wrong";
                responseViewModel.Success = false;
            }

            responseViewModel.Message = "This message has been flagged";
            responseViewModel.Success = true;

            return responseViewModel;




        }
    }
}
