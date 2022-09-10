using AutoMapper;
using MyTravelJournal.Api.DTOs;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserRegisterDto, User>();
        CreateMap<UserLoginDto, User>();
        CreateMap<TripUpdateDto, Trip>();
    }
}