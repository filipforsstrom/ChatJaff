using AutoMapper;
using ChatJaffApp.Server.Chat.ChatRoom.Controllers;
using ChatJaffApp.Server.Chat.ChatRoom.Models;
using ChatJaffApp.Server.Chat.Member.Controllers;
using ChatJaffApp.Server.Chat.Member.Models;
using ChatJaffApp.Server.Identity.Models;
using static ChatJaffApp.Server.Chat.ChatRoom.Controllers.ChatRoomController;

namespace ChatJaffApp.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateChatDTO, MockChatModel>();
            CreateMap<RegisterRequest, ApplicationUser>();
            CreateMap<AddMemberResponse, AddMemberDto>();
        }
    }
}
