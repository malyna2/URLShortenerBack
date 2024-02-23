using AutoMapper;
using URLShortener.Data.Entities;
using URLShortener.Business.DTOs;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserRequestDTO, User>();
    }
}
