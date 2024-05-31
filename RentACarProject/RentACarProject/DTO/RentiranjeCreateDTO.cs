

namespace RentACarProject.DTO
{
    public class RentiranjeCreateDTO
    {
        public decimal BrojDanaIzdavanja { get; set; }
        public DateTime DatumPocetkaIzdavanja { get; set; }
        public DateTime DatumKrajaIzdavanja { get; set; }
        public decimal UkupnaCenaRentiranja { get; set; }
        public string PlacenoR { get; set; } = null!;
        public DateTime? DatumPlacanja { get; set; }
        public decimal? PristupniKod { get; set; }
        public Guid KorisnikId { get; set; }
        public Guid ZaposleniId { get; set; }
        public Guid AutomobilId { get; set; }
    }
}
