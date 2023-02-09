using AutoMapper;
using ChatJaffApp.Server.ChatRoom.Controllers;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.ChatRoom.Member.Controllers;
using ChatJaffApp.Server.ChatRoom.Member.Models;
using ChatJaffApp.Server.Identity.Models;
using static ChatJaffApp.Server.ChatRoom.Controllers.ChatRoomController;
using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, ApplicationUser>();
            CreateMap<MessageDto, Message>();
        }
    }
}
