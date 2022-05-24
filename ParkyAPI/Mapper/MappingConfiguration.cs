using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs.NationalPark;

namespace ParkyAPI.Mapper
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}
