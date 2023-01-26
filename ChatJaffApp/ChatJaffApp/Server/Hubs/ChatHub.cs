using Microsoft.AspNetCore.SignalR;
using System;
namespace ChatJaffApp.Server.Hubs
{
	public class ChatHub : Hub
	{
        public async Task SendMessageAsync(string userName,string message)
        {
            
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
            
        }
        //public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        //{
        //    await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        //}
    }
}

