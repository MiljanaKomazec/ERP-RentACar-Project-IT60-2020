using AutoMapper;
using RentACarProject.DTO;
using RentACarProject.Models;


namespace RentACarProject.Profiles
{
    public class RentiranjeProfile : Profile
    {
        
        public RentiranjeProfile()
        {
            CreateMap<Rentiranje, RentiranjeDTO>()
               .ForMember(dest => dest.BrojDanaIzdavanja, opt => opt.MapFrom(src => src.BrojDanaIzdavanja))
               .ForMember(dest => dest.DatumPocetkaIzdavanja, opt => opt.MapFrom(src => src.DatumPocetkaIzdavanja))
               .ForMember(dest => dest.DatumKrajaIzdavanja, opt => opt.MapFrom(src => src.DatumKrajaIzdavanja))
               .ForMember(dest => dest.UkupnaCenaRentiranja, opt => opt.MapFrom(src => src.UkupnaCenaRentiranja))
               .ForMember(dest => dest.PlacenoR, opt => opt.MapFrom(src => src.PlacenoR))
               .ForMember(dest => dest.DatumPlacanja, opt => opt.MapFrom(src => src.DatumPlacanja))
               .ForMember(dest => dest.PristupniKod, opt => opt.MapFrom(src => src.PristupniKod))
               .ForMember(dest => dest.KorisnikId, opt => opt.MapFrom(src => src.KorisnikId))
               .ForMember(dest => dest.Korisnik, opt => opt.MapFrom(src => src.Korisnik))
               .ForMember(dest => dest.ZaposleniId, opt => opt.MapFrom(src => src.ZaposleniId))
               .ForMember(dest => dest.Zaposleni, opt => opt.MapFrom(src => src.Zaposleni))
               .ForMember(dest => dest.AutomobilId, opt => opt.MapFrom(src => src.AutomobilId))
               .ForMember(dest => dest.Automobil, opt => opt.MapFrom(src => src.Automobil))
               .ReverseMap();
            CreateMap<Rentiranje, RentiranjeCreateDTO>()
               .ForMember(dest => dest.BrojDanaIzdavanja, opt => opt.MapFrom(src => src.BrojDanaIzdavanja))
               .ForMember(dest => dest.DatumPocetkaIzdavanja, opt => opt.MapFrom(src => src.DatumPocetkaIzdavanja))
               .ForMember(dest => dest.DatumKrajaIzdavanja, opt => opt.MapFrom(src => src.DatumKrajaIzdavanja))
               .ForMember(dest => dest.UkupnaCenaRentiranja, opt => opt.MapFrom(src => src.UkupnaCenaRentiranja))
               .ForMember(dest => dest.PlacenoR, opt => opt.MapFrom(src => src.PlacenoR))
               .ForMember(dest => dest.DatumPlacanja, opt => opt.MapFrom(src => src.DatumPlacanja))
               .ForMember(dest => dest.PristupniKod, opt => opt.MapFrom(src => src.PristupniKod))
               .ForMember(dest => dest.KorisnikId, opt => opt.MapFrom(src => src.KorisnikId))
               .ForMember(dest => dest.ZaposleniId, opt => opt.MapFrom(src => src.ZaposleniId))
               .ForMember(dest => dest.AutomobilId, opt => opt.MapFrom(src => src.AutomobilId))
               .ReverseMap();
            CreateMap<Rentiranje, RentiranjeUpdateDTO>()
              .ForMember(dest => dest.RentiranjeId, opt => opt.MapFrom(src => src.RentiranjeId))
              .ForMember(dest => dest.BrojDanaIzdavanja, opt => opt.MapFrom(src => src.BrojDanaIzdavanja))
               .ForMember(dest => dest.DatumPocetkaIzdavanja, opt => opt.MapFrom(src => src.DatumPocetkaIzdavanja))
               .ForMember(dest => dest.DatumKrajaIzdavanja, opt => opt.MapFrom(src => src.DatumKrajaIzdavanja))
               .ForMember(dest => dest.UkupnaCenaRentiranja, opt => opt.MapFrom(src => src.UkupnaCenaRentiranja))
               .ForMember(dest => dest.PlacenoR, opt => opt.MapFrom(src => src.PlacenoR))
               .ForMember(dest => dest.DatumPlacanja, opt => opt.MapFrom(src => src.DatumPlacanja))
               .ForMember(dest => dest.PristupniKod, opt => opt.MapFrom(src => src.PristupniKod))
               
               /*
               .ForMember(dest => dest.KorisnikId, opt => opt.MapFrom(src => src.KorisnikId))
               .ForMember(dest => dest.Korisnik, opt => opt.MapFrom(src => src.Korisnik))
               .ForMember(dest => dest.ZaposleniId, opt => opt.MapFrom(src => src.ZaposleniId))
               .ForMember(dest => dest.Zaposleni, opt => opt.MapFrom(src => src.Zaposleni))
               .ForMember(dest => dest.AutomobilId, opt => opt.MapFrom(src => src.AutomobilId))
               .ForMember(dest => dest.Automobil, opt => opt.MapFrom(src => src.Automobil)) */
              
               .ReverseMap();
            CreateMap<Rentiranje, RentiranjeDostupnostDTO>()
               .ForMember(dest => dest.DatumPocetkaIzdavanja, opt => opt.MapFrom(src => src.DatumPocetkaIzdavanja))
               .ForMember(dest => dest.DatumKrajaIzdavanja, opt => opt.MapFrom(src => src.DatumKrajaIzdavanja))
               .ReverseMap();
            CreateMap<Rentiranje, Rentiranje>(); 

        } 
    }
}
