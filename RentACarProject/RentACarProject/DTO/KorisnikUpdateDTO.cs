namespace RentACarProject.DTO
{
    public class KorisnikUpdateDTO
    {
        public Guid KorisnikId { get; set; }
        public string? JmbgK { get; set; }
        public string ImeK { get; set; } = null!;
        public string PrzK { get; set; } = null!;
        public string? GradK { get; set; }
        public string? AdresaK { get; set; }
        public string KontaktK { get; set; } = null!;
        public string Uloga { get; set; } = null!;
        public string UserNameK { get; set; } = null!;
        public string PasswordK { get; set; } = null!;
    }
}
