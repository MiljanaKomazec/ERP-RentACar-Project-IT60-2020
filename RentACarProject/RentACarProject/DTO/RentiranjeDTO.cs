using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RentACarProject.Models;

namespace RentACarProject.DTO
{
    public class RentiranjeDTO
    {
        
        public decimal BrojDanaIzdavanja { get; set; }
        public DateTime DatumPocetkaIzdavanja { get; set; }
        public DateTime DatumKrajaIzdavanja { get; set; }
        public decimal UkupnaCenaRentiranja { get; set; }
        public string PlacenoR { get; set; } = null!;
        public DateTime? DatumPlacanja { get; set; }
        public decimal? PristupniKod { get; set; }
        public Guid KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; } = null!;
        public Guid ZaposleniId { get; set; }
        public virtual Korisnik Zaposleni { get; set; } = null!;
        public Guid AutomobilId { get; set; }
        public virtual Automobil Automobil { get; set; } = null!; 
    }
}
