using RentACarProject.Models;

namespace RentACarProject.Repository.RentiranjeRepository
{
    public interface IRentiranjeRepository
    {
        Task<List<Rentiranje>> GetRentiranje();
        Task<Rentiranje> GetRentiranjeById(Guid RentiranjeId);
        Task<Rentiranje> CreateRentiranje(Rentiranje rentiranje);
        Task<Rentiranje> UpdateRentiranje(Rentiranje rentiranje);
        //Task UpdateRentiranje(Rentiranje rentiranje);
        Task DeleteRentiranje(Guid RentiranjeId);

        Task<List<Rentiranje>> GetRentiranjeByKorisnikId(Guid KorisnikId);
        Task<bool> SaveChanges();
    }
}
