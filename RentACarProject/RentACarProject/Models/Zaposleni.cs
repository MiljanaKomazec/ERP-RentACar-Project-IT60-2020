using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace RentACarProject.Models
{
    [Table("Zaposleni", Schema = "RentACar")]
    [Index(nameof(JmbgZ), Name = "UQ_jmbgZ", IsUnique = true)]
    public partial class Zaposleni
    {
        public Zaposleni()
        {
            Rentiranjes = new HashSet<Rentiranje>();
        }

        [Key]
        [Column("zaposleniId")]
        public Guid ZaposleniId { get; set; }
        [Column("jmbgZ")]
        [StringLength(13)]
        [Unicode(false)]
        public string? JmbgZ { get; set; }
        [Column("imeZ")]
        [StringLength(20)]
        [Unicode(false)]
        public string ImeZ { get; set; } = null!;
        [Column("przZ")]
        [StringLength(20)]
        [Unicode(false)]
        public string PrzZ { get; set; } = null!;
        [Column("gradZ")]
        [StringLength(30)]
        [Unicode(false)]
        public string? GradZ { get; set; }
        [Column("adresaZ")]
        [StringLength(30)]
        [Unicode(false)]
        public string? AdresaZ { get; set; }
        [Column("ulogaZ")]
        [StringLength(15)]
        [Unicode(false)]
        public string UlogaZ { get; set; } = null!;
        [Column("userNameZ")]
        [StringLength(20)]
        [Unicode(false)]
        public string UserNameZ { get; set; } = null!;
        [Column("passwordZ")]
        [StringLength(1000)]
        [Unicode(false)]
        public string PasswordZ { get; set; } = null!;

        [Column("saltZ")]
        [StringLength(1000)]
        [Unicode(false)]
        public string SaltZ { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty(nameof(Rentiranje.Zaposleni))]
        public virtual ICollection<Rentiranje> Rentiranjes { get; set; }
    }
}
