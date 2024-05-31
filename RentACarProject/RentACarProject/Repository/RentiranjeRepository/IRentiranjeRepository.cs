

using RentACarProject.DTO;
using RentACarProject.Models;

namespace RentACarProject.Repository.RentiranjeRepository
{
    public interface IRentiranjeRepository
    {
        Task<PaginatedRentiranjeDTO> GetRentiranje(int page, int pageSize);
        Task<Rentiranje> GetRentiranjeById(Guid RentiranjeId);
        Task<Rentiranje> CreateRentiranje(Rentiranje rentiranje);
        Task<Rentiranje> UpdateRentiranje(Rentiranje rentiranje);
        //Task UpdateRentiranje(Rentiranje rentiranje);
        Task DeleteRentiranje(Guid RentiranjeId);

        Task<List<Rentiranje>> GetRentiranjeByKorisnikId(Guid KorisnikId);
        Task<List<Rentiranje>> GetRentiranjeByAutomobilId(Guid AutomobilId);
        Task<bool> SaveChanges();
    }
}
