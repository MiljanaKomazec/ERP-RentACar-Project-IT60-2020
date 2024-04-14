using AutoMapper;
using RentACarProject.DTO;
using RentACarProject.Models;

namespace RentACarProject.Profiles
{
    public class ZaposleniProfile : Profile
    {
        public ZaposleniProfile() {

            CreateMap<Zaposleni, ZaposleniDTO>()
                .ForMember(dest => dest.ZaposleniId, opt => opt.MapFrom(src => src.ZaposleniId))
                .ForMember(dest => dest.JmbgZ, opt => opt.MapFrom(src => src.JmbgZ))
                .ForMember(dest => dest.ImeZ, opt => opt.MapFrom(src => src.ImeZ))
                .ForMember(dest => dest.PrzZ, opt => opt.MapFrom(src => src.PrzZ))
                .ForMember(dest => dest.GradZ, opt => opt.MapFrom(src => src.GradZ))
                .ForMember(dest => dest.AdresaZ, opt => opt.MapFrom(src => src.AdresaZ))
                .ForMember(dest => dest.UlogaZ, opt => opt.MapFrom(src => src.UlogaZ))
                .ForMember(dest => dest.UserNameZ, opt => opt.MapFrom(src => src.UserNameZ))
                .ReverseMap();
            CreateMap<Zaposleni, ZaposleniCreateDTO>()
                .ForMember(dest => dest.JmbgZ, opt => opt.MapFrom(src => src.JmbgZ))
                .ForMember(dest => dest.ImeZ, opt => opt.MapFrom(src => src.ImeZ))
                .ForMember(dest => dest.PrzZ, opt => opt.MapFrom(src => src.PrzZ))
                .ForMember(dest => dest.GradZ, opt => opt.MapFrom(src => src.GradZ))
                .ForMember(dest => dest.AdresaZ, opt => opt.MapFrom(src => src.AdresaZ))
                .ForMember(dest => dest.UlogaZ, opt => opt.MapFrom(src => src.UlogaZ))
                .ForMember(dest => dest.UserNameZ, opt => opt.MapFrom(src => src.UserNameZ))
                .ForMember(dest => dest.PasswordZ, opt => opt.MapFrom(src => src.PasswordZ))
                .ReverseMap();
            CreateMap<Zaposleni, ZaposleniUpdateDTO>()
               .ForMember(dest => dest.ZaposleniId, opt => opt.MapFrom(src => src.ZaposleniId))
               .ForMember(dest => dest.JmbgZ, opt => opt.MapFrom(src => src.JmbgZ))
               .ForMember(dest => dest.ImeZ, opt => opt.MapFrom(src => src.ImeZ))
               .ForMember(dest => dest.PrzZ, opt => opt.MapFrom(src => src.PrzZ))
               .ForMember(dest => dest.GradZ, opt => opt.MapFrom(src => src.GradZ))
               .ForMember(dest => dest.AdresaZ, opt => opt.MapFrom(src => src.AdresaZ))
               .ForMember(dest => dest.UlogaZ, opt => opt.MapFrom(src => src.UlogaZ))
               .ForMember(dest => dest.UserNameZ, opt => opt.MapFrom(src => src.UserNameZ))
               .ForMember(dest => dest.PasswordZ, opt => opt.MapFrom(src => src.PasswordZ))
               .ReverseMap();
            CreateMap<Zaposleni, Zaposleni>();
        }
    }
}
