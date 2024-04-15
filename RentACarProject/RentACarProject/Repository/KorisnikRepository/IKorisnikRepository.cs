using RentACarProject.Models;

namespace RentACarProject.Repository.KorisnikRepository
{
    public interface IKorisnikRepository
    {
        List<Korisnik> GetKorisnik();
        Korisnik GetKorisnikById(Guid KorisnikId);
        Korisnik GetKorisnikByUserName(string userName);
        Korisnik CreateKorisnik(Korisnik korisnik);
        Korisnik UpdateKorisnik(Korisnik korisnik);
        void DeleteKorisnik(Guid KorisnikId);
        public bool KorisnikWithCedentialsExists(string username, string password);
        public bool SaveChanges();
    }
}
