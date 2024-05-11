using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace RentACarProject.Models
{
    [Table("Automobil", Schema = "RentACar")]
    public partial class Automobil
    {
        public Automobil()
        {
            Rentiranjes = new HashSet<Rentiranje>();
        }

        [Key]
        [Column("automobilId")]
        public Guid AutomobilId { get; set; }
        [Column("brojSasije")]
        [StringLength(50)]
        [Unicode(false)]
        public string BrojSasije { get; set; } = null!;
        [Column("kubikaza", TypeName = "numeric(10, 0)")]
        public decimal Kubikaza { get; set; }
        [Column("konjskaSnaga", TypeName = "numeric(10, 0)")]
        public decimal KonjskaSnaga { get; set; }
        [Column("godisteAutomobila", TypeName = "numeric(4, 0)")]
        public decimal GodisteAutomobila { get; set; }
        [Column("tipMenjaca")]
        [StringLength(10)]
        [Unicode(false)]
        public string TipMenjaca { get; set; } = null!;
        [Column("tipAutomobila")]
        [StringLength(20)]
        [Unicode(false)]
        public string TipAutomobila { get; set; } = null!;
        [Column("markaAutomobila")]
        [StringLength(20)]
        [Unicode(false)]
        public string MarkaAutomobila { get; set; } = null!;
        [Column("modelAutomobila")]
        [StringLength(20)]
        [Unicode(false)]
        public string ModelAutomobila { get; set; } = null!;
        [Column("cenaPoDanu", TypeName = "numeric(10, 0)")]
        public decimal CenaPoDanu { get; set; }
        [Column("imageUrl")]
        [StringLength(300)]
        [Unicode(false)]
        public string imageUrl { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty(nameof(Rentiranje.Automobil))]
        public virtual ICollection<Rentiranje> Rentiranjes { get; set; }
    }
}
