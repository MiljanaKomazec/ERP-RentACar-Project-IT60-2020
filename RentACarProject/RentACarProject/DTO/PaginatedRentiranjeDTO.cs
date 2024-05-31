using RentACarProject.Models;

namespace RentACarProject.DTO
{
    public class PaginatedRentiranjeDTO
    {
        public List<Rentiranje> Rentiranja { get; set; }
        public int TotalCount { get; set; }
    }
}
