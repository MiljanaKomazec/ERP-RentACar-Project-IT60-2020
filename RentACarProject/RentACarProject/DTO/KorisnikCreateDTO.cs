﻿namespace RentACarProject.DTO
{
    public class KorisnikCreateDTO
    {
        public string? JmbgK { get; set; }
        public string ImeK { get; set; } = null!;
        public string PrzK { get; set; } = null!;
        public string? GradK { get; set; }
        public string? AdresaK { get; set; }
        public string KontaktK { get; set; } = null!;
        public string UserNameK { get; set; } = null!;
        public string PasswordK { get; set; } = null!;
    }
}
