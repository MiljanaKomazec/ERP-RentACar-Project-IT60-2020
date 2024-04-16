using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.DTO
{
    public class KorisnikDTO
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

    }
}
