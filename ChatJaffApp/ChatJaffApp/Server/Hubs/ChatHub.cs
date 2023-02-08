using ChatJaffApp.Client.ChatRoom.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Text.Json;

namespace ChatJaffApp.Server.Hubs
{
    [Authorize]
	public class ChatHub : Hub
	{
        

        public async Task SendMessageAsync(string message, Guid chatroomId)
        {
            var deserializedMessage = JsonSerializer.Deserialize<MessageDto>(message);
            deserializedMessage.Sent = DateTime.UtcNow;
            deserializedMessage.ChatroomId = chatroomId;
            

            //save to db?

            var serializedResponse = JsonSerializer.Serialize(deserializedMessage);
            await Clients.Groups(chatroomId.ToString()).SendAsync("ReceiveMessage", serializedResponse);

        }

        public async Task AddToGroup(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);

            var group = Groups;

            var userName = Context.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;

            await Clients.Group(chatRoomId).SendAsync("MemberJoined", $"{userName} has joined the group.");
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
        public Guid ChatroomId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }        
        public DateTime Sent { get; set; }
    }

}

