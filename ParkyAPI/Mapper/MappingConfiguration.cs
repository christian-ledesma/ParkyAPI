using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs.NationalPark;
using ParkyAPI.Models.DTOs.Trail;

namespace ParkyAPI.Mapper
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
        }
    }
}
