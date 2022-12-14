using AutoMapper;
using CarMeetApp.Api.Dtos;
using CarMeetApp.Domain.Models;

namespace CarMeetApp.Api.AutoMapper
{
    public class CarMappingProfiles : Profile
    {
        public CarMappingProfiles()
        {
            CreateMap<Car, CarReadDto>();
            CreateMap<CarCreateDto, Car>();
        }
    }
}