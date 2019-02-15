using AutoMapper;
using GreatBear.Demo.Application.Users.Dto;
using GreatBear.Demo.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Demo.Application.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
