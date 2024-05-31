using AutoMapper;
using RentACarProject.DTO;
using RentACarProject.Models;


namespace RentACarProject.Profiles
{
    public class KorinsikProfile : Profile
    { 
        public KorinsikProfile() {
            CreateMap<Korisnik, KorisnikDTO>()
                .ForMember(dest => dest.KorisnikId, opt => opt.MapFrom(src => src.KorisnikId))
                .ForMember(dest => dest.JmbgK, opt => opt.MapFrom(src => src.JmbgK))
                .ForMember(dest => dest.ImeK, opt => opt.MapFrom(src => src.ImeK))
                .ForMember(dest => dest.PrzK, opt => opt.MapFrom(src => src.PrzK))
                .ForMember(dest => dest.GradK, opt => opt.MapFrom(src => src.GradK))
                .ForMember(dest => dest.AdresaK, opt => opt.MapFrom(src => src.AdresaK))
                .ForMember(dest => dest.KontaktK, opt => opt.MapFrom(src => src.KontaktK))
                .ForMember(dest => dest.Uloga, opt => opt.MapFrom(src => src.Uloga))
                .ForMember(dest => dest.UserNameK, opt => opt.MapFrom(src => src.UserNameK))
                .ReverseMap();
            CreateMap<Korisnik, KorisnikCreateDTO>()
                .ForMember(dest => dest.JmbgK, opt => opt.MapFrom(src => src.JmbgK))
                .ForMember(dest => dest.ImeK, opt => opt.MapFrom(src => src.ImeK))
                .ForMember(dest => dest.PrzK, opt => opt.MapFrom(src => src.PrzK))
                .ForMember(dest => dest.GradK, opt => opt.MapFrom(src => src.GradK))
                .ForMember(dest => dest.AdresaK, opt => opt.MapFrom(src => src.AdresaK))
                .ForMember(dest => dest.KontaktK, opt => opt.MapFrom(src => src.KontaktK))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserNameK))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordK))
                .ReverseMap();
            CreateMap<Korisnik, KorisnikUpdateDTO>()
               .ForMember(dest => dest.KorisnikId, opt => opt.MapFrom(src => src.KorisnikId))
                .ForMember(dest => dest.JmbgK, opt => opt.MapFrom(src => src.JmbgK))
                .ForMember(dest => dest.ImeK, opt => opt.MapFrom(src => src.ImeK))
                .ForMember(dest => dest.PrzK, opt => opt.MapFrom(src => src.PrzK))
                .ForMember(dest => dest.GradK, opt => opt.MapFrom(src => src.GradK))
                .ForMember(dest => dest.AdresaK, opt => opt.MapFrom(src => src.AdresaK))
                .ForMember(dest => dest.KontaktK, opt => opt.MapFrom(src => src.KontaktK))
                .ForMember(dest => dest.Uloga, opt => opt.MapFrom(src => src.Uloga))
                .ForMember(dest => dest.UserNameK, opt => opt.MapFrom(src => src.UserNameK))
               .ForMember(dest => dest.PasswordK, opt => opt.MapFrom(src => src.PasswordK))
               .ReverseMap();
            CreateMap<Korisnik, Korisnik>();



        } 
    }
}
