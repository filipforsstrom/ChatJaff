using System.Net.Http.Json;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data;
using Microsoft.AspNetCore.Hosting;

namespace ChatJaffApi.Tests
{
    public class ChatRoomControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ChatRoomControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task POST_CreateChat_ReturnCreatedChatId()
        {
            //Arrange
            var httpClient = _factory.CreateClient();
            var chatRequest = new CreateChatDTO { ChatName = "Test Chat", Encrypted = false, ChatMembersIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() } };
           
            //Act
            var response = await httpClient.PostAsJsonAsync("api/chatroom/createchat", chatRequest);

            //Assert
            response.Should().BeSuccessful();
        }

        [Fact]
        public async Task POST_CreateChat_ChatDtoEmpty_ReturnBadRequest()
        {
            //Arrange
            var httpClient = _factory.CreateClient();
            var chatRequest = new CreateChatDTO {};
            //Act
            var response = await httpClient.PostAsJsonAsync("api/chatroom/createchat", chatRequest);

            //Assert
            response.Should().HaveClientError();
        }

        //[Fact]
        //public async Task PATCH_AddMemberToChat_WhenUserExists_ReturnOk()
        //{
        //    //Arrange
        //    var httpClient = _factory.CreateClient();
        //    var userId = Guid.NewGuid();

        //    //Act
        //    var response = await httpClient.PatchAsJsonAsync($"api/chatroom/{}", );

        //    //Assert
        //    response.Should().HaveClientError();
        //}
    }
    
}