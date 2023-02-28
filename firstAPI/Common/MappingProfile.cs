using AutoMapper;
using firstAPI.DTO.Country;
using firstAPI.Models;

namespace firstAPI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // source, dest
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country,CountryDto>().ReverseMap();
            CreateMap<Country,UpdateCountryDto>().ReverseMap();
        }
    }
}
