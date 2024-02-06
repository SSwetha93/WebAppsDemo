using AutoMapper;
using VillaMVCProj.Models;
using VillaMVCProj.Models.Dto;

namespace VillaMVCProj
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaCreateDTO, VillaDTO>().ReverseMap();
            CreateMap<VillaUpdateDTO, VillaDTO>().ReverseMap();

            CreateMap<VillaNumberUpdateDTO, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumberDTO>().ReverseMap();
        }
    }
}
