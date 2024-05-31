using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace RentACarProject.Models
{
    [Table("Korisnik", Schema = "RentACar")]
    [Index(nameof(JmbgK), Name = "UQ_jmbgK", IsUnique = true)]
    public partial class Korisnik
    {
        public Korisnik()
        {
            RentiranjeKorisniks = new HashSet<Rentiranje>();
            RentiranjeZaposlenis = new HashSet<Rentiranje>();
        }

        [Key]
        [Column("korisnikId")]
        public Guid KorisnikId { get; set; }
        [Column("jmbgK")]
        [StringLength(13)]
        [Unicode(false)]
        public string? JmbgK { get; set; }
        [Column("imeK")]
        [StringLength(20)]
        [Unicode(false)]
        public string ImeK { get; set; } = null!;
        [Column("przK")]
        [StringLength(20)]
        [Unicode(false)]
        public string PrzK { get; set; } = null!;
        [Column("gradK")]
        [StringLength(30)]
        [Unicode(false)]
        public string? GradK { get; set; }
        [Column("adresaK")]
        [StringLength(30)]
        [Unicode(false)]
        public string? AdresaK { get; set; }
        [Column("kontaktK")]
        [StringLength(30)]
        [Unicode(false)]
        public string KontaktK { get; set; } = null!;
        [Column("uloga")]
        [StringLength(15)]
        [Unicode(false)]
        public string Uloga { get; set; } = null!;
        [Column("userNameK")]
        [StringLength(20)]
        [Unicode(false)]
        public string UserNameK { get; set; } = null!;
        [Column("passwordK")]
        [StringLength(1000)]
        [Unicode(false)]
        public string PasswordK { get; set; } = null!;
        [Column("saltK")]
        [StringLength(1000)]
        [Unicode(false)]
        public string SaltK { get; set; } = null!;
        [JsonIgnore]
        [InverseProperty(nameof(Rentiranje.Korisnik))]
        public virtual ICollection<Rentiranje> RentiranjeKorisniks { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(Rentiranje.Zaposleni))]
        public virtual ICollection<Rentiranje> RentiranjeZaposlenis { get; set; }
    }
}
