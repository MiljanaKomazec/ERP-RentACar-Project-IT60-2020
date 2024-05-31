using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.DTO
{
    public class AutomobilDTO
    {
        public string BrojSasije { get; set; } = null!;
        public decimal Kubikaza { get; set; }
        public decimal KonjskaSnaga { get; set; }
        public decimal GodisteAutomobila { get; set; }
        public string TipMenjaca { get; set; } = null!;
        public string TipAutomobila { get; set; } = null!;
        public string MarkaAutomobila { get; set; } = null!;
        public string ModelAutomobila { get; set; } = null!;
        public decimal CenaPoDanu { get; set; }
        public string imageUrl { get; set; } = null!;
    }
}
