namespace RentACarProject.DTO
{
    public class ZaposleniCreateDTO
    {
        public string? JmbgZ { get; set; }
        public string ImeZ { get; set; } = null!;
        public string PrzZ { get; set; } = null!;
        public string? GradZ { get; set; }
        public string? AdresaZ { get; set; }
        public string UlogaZ { get; set; } = null!;
        public string UserNameZ { get; set; } = null!;
        public string PasswordZ { get; set; } = null!;
    }
}
