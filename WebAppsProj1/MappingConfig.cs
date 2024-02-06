using AutoMapper;
using WebAppsProj1.Models;
using WebAppsProj1.Models.Dto;

namespace WebAppsProj1
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            CreateMap<VillaUpdateDTO, Villa>().ReverseMap();
            CreateMap<VillaCreateDTO, Villa>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberUpdateDTO, VillaNumber>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumber>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
