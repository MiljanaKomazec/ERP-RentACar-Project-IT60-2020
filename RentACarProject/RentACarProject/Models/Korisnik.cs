using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RentACarProject.Models
{
    [Table("Korisnik", Schema = "RentACar")]
    [Index(nameof(JmbgK), Name = "UQ_jmbgK", IsUnique = true)]
    public partial class Korisnik
    {
        public Korisnik()
        {
            Rentiranjes = new HashSet<Rentiranje>();
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
        [Column("userNameK")]
        [StringLength(20)]
        [Unicode(false)]
        public string UserNameK { get; set; } = null!;
        [Column("passwordK")]
        [StringLength(20)]
        [Unicode(false)]
        public string PasswordK { get; set; } = null!;

        [InverseProperty(nameof(Rentiranje.Korisnik))]
        public virtual ICollection<Rentiranje> Rentiranjes { get; set; }
    }
}
