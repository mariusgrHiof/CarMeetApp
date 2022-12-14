using AutoMapper;
using CarMeetApp.Api.Dtos;
using CarMeetApp.Domain.Models;

namespace CarMeetApp.Api.AutoMapper
{
    public class UserMappingProfiles : Profile
    {
        public UserMappingProfiles()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}