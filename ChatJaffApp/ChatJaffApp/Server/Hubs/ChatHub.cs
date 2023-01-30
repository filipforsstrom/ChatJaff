using ChatJaffApp.Client.ChatRoom.Pages;
using Microsoft.AspNetCore.SignalR;
using System;
namespace ChatJaffApp.Server.Hubs
{
	public class ChatHub : Hub
	{
        

        public async Task SendMessageAsync(string userName,string message, Guid chatroomId)
        {

            await Clients.Groups(chatroomId.ToString()).SendAsync("ReceiveMessage", userName, message);
            //await Clients.All.SendAsync("ReceiveMessage", userName, message);

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
}

