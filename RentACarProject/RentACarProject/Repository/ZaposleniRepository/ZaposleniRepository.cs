using AutoMapper;
using RentACarProject.Data;
using RentACarProject.Models;
using System.Security.Cryptography;

namespace RentACarProject.Repository.ZaposleniRepository
{
    public class ZaposleniRepository : IZaposleniRepository
    {
        private readonly RentACarContext context;
        private readonly IMapper mapper;
        private readonly static int iterations = 1000;

        public ZaposleniRepository(RentACarContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Zaposleni> GetZaposleni()
        {
            return context.Zaposlenis.ToList();
        }

        public Zaposleni GetZaposleniById(Guid ZaposleniId)
        {
            return context.Zaposlenis.FirstOrDefault(e => e.ZaposleniId == ZaposleniId);
        }

        public Zaposleni CreateZaposleni(Zaposleni zaposleni)
        {
            var password = HashPassword(zaposleni.PasswordZ);
            zaposleni.ZaposleniId = new Guid();
            zaposleni.PasswordZ = password.Item1;
            zaposleni.SaltZ = password.Item2;
            var createdEntity = context.Add(zaposleni);
            return mapper.Map<Zaposleni>(createdEntity.Entity);
        }

        public void DeleteZaposleni(Guid ZaposleniId)
        {
            var zaposleni = GetZaposleniById(ZaposleniId);
            context.Remove(zaposleni);
        }

        public Zaposleni GetZaposleniByUserName(string userName)
        {
            return context.Zaposlenis.FirstOrDefault(e => e.UserNameZ == userName);
        }

        public Zaposleni UpdateZaposleni(Zaposleni zaposleni)
        {
            try
            {
                var existingZaposleni = context.Zaposlenis.FirstOrDefault(e => e.ZaposleniId == zaposleni.ZaposleniId);

                if (existingZaposleni != null)
                {
                    if (zaposleni.PasswordZ != null && VerifyPassword(zaposleni.PasswordZ, existingZaposleni.PasswordZ, existingZaposleni.SaltZ) == false)
                    {
                        Tuple<string, string> newPassword = HashPassword(zaposleni.PasswordZ);
                        existingZaposleni.PasswordZ = newPassword.Item1;
                        existingZaposleni.SaltZ = newPassword.Item2;

                    }

                    existingZaposleni.ZaposleniId = zaposleni.ZaposleniId;
                    existingZaposleni.JmbgZ = zaposleni.JmbgZ;
                    existingZaposleni.ImeZ = zaposleni.ImeZ;
                    existingZaposleni.PrzZ = zaposleni.PrzZ;
                    existingZaposleni.GradZ = zaposleni.GradZ;
                    existingZaposleni.AdresaZ = zaposleni.AdresaZ;
                    existingZaposleni.UlogaZ = zaposleni.UlogaZ;
                    existingZaposleni.UserNameZ = zaposleni.UserNameZ;

                    context.SaveChanges();
                    return mapper.Map<Zaposleni>(existingZaposleni);
                }

                else
                {
                    throw new KeyNotFoundException($"Zaposleni with ID {zaposleni.ZaposleniId} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public bool ZaposleniWithCedentialsExists(string username, string password)
        {
            Zaposleni zaposleni = context.Zaposlenis.FirstOrDefault(e => e.UserNameZ == username);
            if (zaposleni == null)
            {
                return false;
            }

            if (VerifyPassword(password, zaposleni.PasswordZ, zaposleni.SaltZ))
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
