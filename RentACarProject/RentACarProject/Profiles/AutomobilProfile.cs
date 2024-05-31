using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.DTO;
using RentACarProject.Models;


namespace RentACarProject.Profiles
{
    public class AutomobilProfile : Profile
    { 
        public AutomobilProfile() {
            CreateMap<Automobil, AutomobilDTO>()
                .ForMember(dest => dest.BrojSasije, opt => opt.MapFrom(src => src.BrojSasije))
                .ForMember(dest => dest.Kubikaza, opt => opt.MapFrom(src => src.Kubikaza))
                .ForMember(dest => dest.KonjskaSnaga, opt => opt.MapFrom(src => src.KonjskaSnaga))
                .ForMember(dest => dest.GodisteAutomobila, opt => opt.MapFrom(src => src.GodisteAutomobila))
                .ForMember(dest => dest.TipMenjaca, opt => opt.MapFrom(src => src.TipMenjaca))
                .ForMember(dest => dest.TipAutomobila, opt => opt.MapFrom(src => src.TipAutomobila))
                .ForMember(dest => dest.MarkaAutomobila, opt => opt.MapFrom(src => src.MarkaAutomobila))
                .ForMember(dest => dest.ModelAutomobila, opt => opt.MapFrom(src => src.ModelAutomobila))
                .ForMember(dest => dest.CenaPoDanu, opt => opt.MapFrom(src => src.CenaPoDanu))
                .ForMember(dest => dest.imageUrl, opt => opt.MapFrom(src => src.imageUrl))
                .ReverseMap();
            CreateMap<Automobil, Automobil>();
        }
        
    }
}
