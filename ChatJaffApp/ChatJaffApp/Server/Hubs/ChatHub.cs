﻿using ChatJaffApp.Client.ChatRoom.Pages;
using Microsoft.AspNetCore.Authorization;
﻿using AutoMapper;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Text.Json;
using ChatJaffApp.Server.ChatRoom.Contracts;

namespace ChatJaffApp.Server.Hubs
{
    [Authorize]
	public class ChatHub : Hub
	{
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public ChatHub(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(string message, Guid chatroomId)
        {
            var deserializedMessage = JsonSerializer.Deserialize<MessageDto>(message);
            deserializedMessage.Sent = DateTime.UtcNow;
            deserializedMessage.ChatroomId = chatroomId;
            

            //save to db?
            var messageToStore = _mapper.Map<Message>(deserializedMessage);
            messageToStore.ChatId = chatroomId;

            await _messageRepository.AddMessageAsync(messageToStore);

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
}