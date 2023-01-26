﻿using AutoMapper;
using ChatJaffApp.Server.ChatRoom.Controllers;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.ChatRoom.Member.Controllers;
using ChatJaffApp.Server.ChatRoom.Member.Models;
using ChatJaffApp.Server.Identity.Models;
using static ChatJaffApp.Server.ChatRoom.Controllers.ChatRoomController;

namespace ChatJaffApp.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, ApplicationUser>();
            CreateMap<AddMemberResponse, AddMemberDto>();
        }
    }
}
