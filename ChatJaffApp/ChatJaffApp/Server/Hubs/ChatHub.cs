using ChatJaffApp.Client.ChatRoom.Pages;
using Microsoft.AspNetCore.Authorization;
﻿using AutoMapper;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Text.Json;
using ChatJaffApp.Server.ChatRoom.Contracts;
using System.Text;
using ChatJaffApp.Server.ChatRoom.Encryption;

namespace ChatJaffApp.Server.Hubs
{
    [Authorize]
	public class ChatHub : Hub
	{
        private readonly IMessageRepository _messageRepository;
        private readonly IChatKeyRepository _chatKeyRepository;
        private readonly IMapper _mapper;

        public ChatHub(IMessageRepository messageRepository, IChatKeyRepository chatKeyRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _chatKeyRepository = chatKeyRepository;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(string message, Guid chatroomId)
        {
            var deserializedMessage = JsonSerializer.Deserialize<MessageDto>(message);
            deserializedMessage.Sent = DateTime.UtcNow;
            deserializedMessage.ChatroomId = chatroomId;
            

            var messageToStore = _mapper.Map<Message>(deserializedMessage);
            messageToStore.ChatId = chatroomId;

            // if encrypted chat, encrypt message
            if (deserializedMessage.Encrypted)
            {                
                try
                {
                    var chatkey = await _chatKeyRepository.GetChatKeyAsync(chatroomId);
                    messageToStore.Content = EncryptMessage(messageToStore.Content, chatkey);
                }
                catch (Exception)
                {
                    await Clients.Groups(chatroomId.ToString()).SendAsync("ReceiveMessage", JsonSerializer.Serialize(deserializedMessage));
                    return;
                }
            }

            var serializedResponse = JsonSerializer.Serialize(deserializedMessage);
            await Clients.Groups(chatroomId.ToString()).SendAsync("ReceiveMessage", serializedResponse);
            
            await _messageRepository.AddMessageAsync(messageToStore);
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

        private string EncryptMessage(string message, string chatKey)
        {
            
            var keyStringArray = chatKey.Split(".");
            string key = keyStringArray[0];

            // skapa salt
            byte[] salt = Encoding.Unicode.GetBytes(keyStringArray[1]);

            return AesEncryptManager.Encrypt(message, key, salt);
        }
    }
}