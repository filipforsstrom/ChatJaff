using ChatJaffApp.Client.ChatRoom.Pages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Text.Json;
using ChatJaffApp.Server.ChatRoom.Contracts;
using System.Text;
using ChatJaffApp.Server.ChatRoom.Encryption;

namespace ChatJaffApp.Server.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IChatKeyRepository _chatKeyRepository;
    private readonly IMapper _mapper;
    private readonly static ConnectionMapping<Guid> _connections =
            new();

    public ChatHub(IChatRoomRepository chatRoomRepository, IMessageRepository messageRepository, IChatKeyRepository chatKeyRepository, IMapper mapper) 
    {
        _chatRoomRepository = chatRoomRepository;
        _messageRepository = messageRepository;
        _chatKeyRepository = chatKeyRepository;
        _mapper = mapper;
    }
       
    public async Task SendMessageAsync(string message, Guid chatroomId)
    {
        if (Context.UserIdentifier == null)
        {
            return;
        }
        var user = Guid.Parse(Context.UserIdentifier);

        if (!_connections.VerifyGroup(user, chatroomId))
        {
            return;
        }

            
        var deserializedMessage = JsonSerializer.Deserialize<MessageDto>(message);
        deserializedMessage.Sent = DateTime.UtcNow;
        deserializedMessage.ChatroomId = chatroomId;

        var messageToStore = _mapper.Map<Message>(deserializedMessage);
        messageToStore.ChatId = chatroomId;

        if (deserializedMessage.Encrypted)
        {
            try
            {
                var chatkey = await _chatKeyRepository.GetChatKeyAsync(chatroomId);
                    EncryptionHelper encryptionHelper = new();
                messageToStore.Content = encryptionHelper.EncryptMessage(messageToStore.Content, chatkey);
            }
            catch (Exception)
            {
                await Clients.Groups(chatroomId.ToString()).SendAsync("ReceiveMessage", JsonSerializer.Serialize(deserializedMessage));
                return;
            }
        }

        var messageId = await _messageRepository.AddMessageAsync(messageToStore);
        deserializedMessage.Id = messageId;

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

    public override Task OnConnectedAsync()
    {
        if (Context.UserIdentifier == null)
        {
            return base.OnDisconnectedAsync(null);
        }
        var userId = Guid.Parse(Context.UserIdentifier);
        var chatRooms = _chatRoomRepository.GetMyChatRooms(userId);
        var chatRoomIds = chatRooms.Select(c => c.Id).ToList();

        _connections.Add(userId, Context.ConnectionId, chatRoomIds);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.UserIdentifier == null)
        {
            return base.OnDisconnectedAsync(null);
        }
        var userId = Guid.Parse(Context.UserIdentifier);

        _connections.Remove(userId, Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
