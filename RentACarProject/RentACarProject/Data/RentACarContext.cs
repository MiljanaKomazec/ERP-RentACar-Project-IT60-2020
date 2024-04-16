using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentACarProject.Models;

namespace RentACarProject.Data
{
    public partial class RentACarContext : DbContext
    {
        private readonly IConfiguration configuration;
        public RentACarContext()
        {
        }

        public RentACarContext(DbContextOptions<RentACarContext> options)
            : base(options)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<Automobil> Automobils { get; set; } = null!;
        public virtual DbSet<Korisnik> Korisniks { get; set; } = null!;
        public virtual DbSet<Rentiranje> Rentiranjes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("RentACar"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Automobil>(entity =>
            {
                entity.Property(e => e.AutomobilId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.KorisnikId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Rentiranje>(entity =>
            {
                entity.Property(e => e.RentiranjeId).ValueGeneratedNever();

                entity.HasOne(d => d.Automobil)
                    .WithMany(p => p.Rentiranjes)
                    .HasForeignKey(d => d.AutomobilId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Rentiranje_Automobil");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.RentiranjeKorisniks)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Rentiranje_Korisnik");

                entity.HasOne(d => d.Zaposleni)
                    .WithMany(p => p.RentiranjeZaposlenis)
                    .HasForeignKey(d => d.ZaposleniId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Rentiranje_Zaposleni");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
