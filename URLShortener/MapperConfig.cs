using AutoMapper;
using URLShortener.Data.Entities;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserRequestDTO, User>();
    }
}
