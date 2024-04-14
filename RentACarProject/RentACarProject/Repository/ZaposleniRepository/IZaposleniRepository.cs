using RentACarProject.Models;

namespace RentACarProject.Repository.ZaposleniRepository
{
    public interface IZaposleniRepository
    {
        List<Zaposleni> GetZaposleni();
        Zaposleni GetZaposleniById(Guid ZaposleniId);
        Zaposleni GetZaposleniByUserName(string userName);
        Zaposleni CreateZaposleni(Zaposleni zaposleni);
        Zaposleni UpdateZaposleni(Zaposleni zaposleni);
        void DeleteZaposleni(Guid ZaposleniId);
        public bool ZaposleniWithCedentialsExists(string username, string password);
        public bool SaveChanges();
    }
}
