using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RentACarProject.Models
{
    [Table("Rentiranje", Schema = "RentACar")]
    [Index(nameof(PristupniKod), Name = "UQ_pristupniKod", IsUnique = true)]
    public partial class Rentiranje
    {
        [Key]
        [Column("rentiranjeId")]
        public Guid RentiranjeId { get; set; }
        [Column("brojDanaIzdavanja", TypeName = "numeric(5, 0)")]
        public decimal BrojDanaIzdavanja { get; set; }
        [Column("datumPocetkaIzdavanja", TypeName = "date")]
        public DateTime DatumPocetkaIzdavanja { get; set; }
        [Column("datumKrajaIzdavanja", TypeName = "date")]
        public DateTime DatumKrajaIzdavanja { get; set; }
        [Column("ukupnaCenaRentiranja", TypeName = "numeric(13, 0)")]
        public decimal UkupnaCenaRentiranja { get; set; }
        [Column("placenoR")]
        [StringLength(2)]
        [Unicode(false)]
        public string PlacenoR { get; set; } = null!;
        [Column("datumPlacanja", TypeName = "date")]
        public DateTime? DatumPlacanja { get; set; }
        [Column("pristupniKod", TypeName = "numeric(13, 0)")]
        public decimal? PristupniKod { get; set; }
        [Column("korisnikId")]
        public Guid KorisnikId { get; set; }
        [Column("zaposleniId")]
        public Guid ZaposleniId { get; set; }
        [Column("automobilId")]
        public Guid AutomobilId { get; set; }

        [ForeignKey(nameof(AutomobilId))]
        [InverseProperty("Rentiranjes")]
        public virtual Automobil Automobil { get; set; } = null!;
        [ForeignKey(nameof(KorisnikId))]
        [InverseProperty("RentiranjeKorisniks")]
        public virtual Korisnik Korisnik { get; set; } = null!;
        [ForeignKey(nameof(ZaposleniId))]
        [InverseProperty("RentiranjeZaposlenis")]
        public virtual Korisnik Zaposleni { get; set; } = null!;
    }
}
