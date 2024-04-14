using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.DTO
{
    public class ZaposleniDTO
    {
        public Guid ZaposleniId { get; set; }
        public string? JmbgZ { get; set; }
        public string ImeZ { get; set; } = null!;
        public string PrzZ { get; set; } = null!;
        public string? GradZ { get; set; }
        public string? AdresaZ { get; set; }
        public string UlogaZ { get; set; } = null!;
        public string UserNameZ { get; set; } = null!;

    }
}
