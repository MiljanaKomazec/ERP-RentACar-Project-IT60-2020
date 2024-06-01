using RentACarProject.Models;

namespace RentACarProject.DTO
{
    public class PaginatedKorisnikDTO
    {
        public List<Korisnik> Korisnici { get; set; }
        public int TotalCount { get; set; } //zbog paginacije
    }
}
