using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.DTO;
using RentACarProject.Models;
using System.Security.Cryptography;

namespace RentACarProject.Repository.KorisnikRepository
{
   
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly RentACarContext context;
        private readonly IMapper mapper;
        private readonly static int iterations = 1000;

        public KorisnikRepository(RentACarContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public PaginatedKorisnikDTO GetKorisnik(int page, int pageSize)
        {
            try
            {
                var totalCount = context.Korisniks.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var korisnici = context.Korisniks
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return new PaginatedKorisnikDTO
                {
                    Korisnici = korisnici,
                    TotalCount = totalCount
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Korisnik GetKorisnikById(Guid KorisnikId)
        {
            return context.Korisniks.FirstOrDefault(e => e.KorisnikId == KorisnikId);
        }

        public Korisnik GetKorisnikByUserName(string userName)
        {
            return context.Korisniks.FirstOrDefault(e => e.UserNameK == userName);
        }
        public Korisnik CreateKorisnik(Korisnik korisnik)
        {
            var password = HashPassword(korisnik.PasswordK);
            korisnik.KorisnikId = Guid.NewGuid();
            korisnik.PasswordK = password.Item1;
            korisnik.SaltK = password.Item2;
            korisnik.Uloga = "Korisnik";
            var createdEntity = context.Add(korisnik);
            return mapper.Map<Korisnik>(createdEntity.Entity);
        }

        public Korisnik UpdateKorisnik(Korisnik korisnik)
        {
            try
            {
                var existingKorisnik = context.Korisniks.FirstOrDefault(e => e.KorisnikId == korisnik.KorisnikId);

                if (existingKorisnik != null)
                {
                    if (korisnik.PasswordK != null && VerifyPassword(korisnik.PasswordK, existingKorisnik.PasswordK, existingKorisnik.SaltK) == false)
                    {
                        Tuple<string, string> newPassword = HashPassword(korisnik.PasswordK);
                        existingKorisnik.PasswordK = newPassword.Item1;
                        existingKorisnik.SaltK = newPassword.Item2;

                    }

                    existingKorisnik.KorisnikId = korisnik.KorisnikId;
                    existingKorisnik.JmbgK = korisnik.JmbgK;
                    existingKorisnik.ImeK = korisnik.ImeK;
                    existingKorisnik.PrzK = korisnik.PrzK;
                    existingKorisnik.GradK = korisnik.GradK;
                    existingKorisnik.AdresaK = korisnik.AdresaK;
                    existingKorisnik.KontaktK = korisnik.KontaktK;
                    existingKorisnik.Uloga = korisnik.Uloga;
                    existingKorisnik.UserNameK = korisnik.UserNameK;

                    context.SaveChanges();
                    return mapper.Map<Korisnik>(existingKorisnik);
                }

                else
                {
                    throw new KeyNotFoundException($"Korisnik with ID {korisnik.KorisnikId} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public void DeleteKorisnik(Guid KorisnikId)
        {

            var korisnik = GetKorisnikById(KorisnikId);
            context.Remove(korisnik);
        }

        public bool KorisnikWithCedentialsExists(string username, string password)
        {
            Korisnik korisnik = context.Korisniks.FirstOrDefault(e => e.UserNameK == username);
            if (korisnik == null)
            {
                return false;
            }

            if (VerifyPassword(password, korisnik.PasswordK, korisnik.SaltK))
            {
                return true;
            }
            return false;
        }

        private Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }

        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }
    }
}
