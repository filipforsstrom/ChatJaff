using ChatJaffApp.Client.ChatRoom.Pages;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text.Json;

namespace ChatJaffApp.Server.Hubs
{
	public class ChatHub : Hub
	{
        public ChatHub()
        {

        }

        public async Task SendMessageAsync(string userName,string message, Guid chatroomId)
        {
            //var responseMsg = new MessageDto
            //{
            //    Id = new Guid(),
            //    UserName = userName,
            //    Sent = DateTime.UtcNow,
            //    Content = message
            //};

            //var serializedResponse = JsonSerializer.Serialize(responseMsg);
            await Clients.Groups(chatroomId.ToString()).SendAsync("ReceiveMessage", userName, message);
            
        }

        public async Task AddToGroup(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);

            var group = Groups;

            await Clients.Group(chatRoomId).SendAsync("MemberJoined", $"{Context.ConnectionId} has joined the group {chatRoomId}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
        //public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        //{
        //    await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        //}
    }

    public class MessageDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public DateTime Sent { get; set; }
    }

}

