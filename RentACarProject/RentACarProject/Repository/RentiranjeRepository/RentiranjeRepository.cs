using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.Models;


namespace RentACarProject.Repository.RentiranjeRepository
{
    
    public class RentiranjeRepository : IRentiranjeRepository
    {
        private readonly RentACarContext context;
        private readonly IMapper mapper;

        public RentiranjeRepository(RentACarContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<List<Rentiranje>> GetRentiranje(int page, int pageSize)
        {
            try
            {
                var rentiranja = await context.Rentiranjes.Include(k => k.Korisnik)
                                                                        .Include(z => z.Zaposleni)
                                                                        .Include(a => a.Automobil)
                                                                        .ToListAsync();
                var totalCount = rentiranja.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var obj = await context.Rentiranjes
                    .Include(k => k.Korisnik)
                    .Include(z => z.Zaposleni)
                    .Include(a => a.Automobil)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                if (obj != null)
                {
                    foreach (Rentiranje rentiranje in obj)
                    {
                        Console.WriteLine(rentiranje);
                    }
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<Rentiranje> GetRentiranjeById(Guid RentiranjeId)
        {
            var rentiranja = await context.Rentiranjes.Include(k => k.Korisnik)
                                                                        .Include(z => z.Zaposleni)
                                                                        .Include(a => a.Automobil)
                                                                        .ToListAsync();
            return rentiranja.FirstOrDefault(e => e.RentiranjeId == RentiranjeId);                                                          

        }

        public async Task<Rentiranje> CreateRentiranje(Rentiranje rentiranje)
        {
            rentiranje.RentiranjeId = Guid.NewGuid();
            var createdEntity = await context.AddAsync(rentiranje);
            await context.SaveChangesAsync();
            return mapper.Map<Rentiranje>(createdEntity.Entity);
        }

        public async Task<Rentiranje> UpdateRentiranje(Rentiranje rentiranje)
        {
            try
            {
                
                var rentiranja = await context.Rentiranjes.Include(k => k.Korisnik)
                                                                         .Include(z => z.Zaposleni)
                                                                         .Include(a => a.Automobil)
                                                                         .ToListAsync();
                var existingRentiranje = rentiranja.FirstOrDefault(e => e.RentiranjeId == rentiranje.RentiranjeId);
                

                if (existingRentiranje != null)
                {
                    //context.Entry(existingRentiranje.Automobil).State = EntityState.Detached;
                    //context.Entry(existingRentiranje.Korisnik).State = EntityState.Detached;
                    //context.Entry(existingRentiranje.Zaposleni).State = EntityState.Detached;

                    existingRentiranje.RentiranjeId = rentiranje.RentiranjeId;
                    existingRentiranje.BrojDanaIzdavanja = rentiranje.BrojDanaIzdavanja;
                    existingRentiranje.DatumPocetkaIzdavanja = rentiranje.DatumPocetkaIzdavanja;
                    existingRentiranje.DatumKrajaIzdavanja = rentiranje.DatumKrajaIzdavanja;
                    existingRentiranje.UkupnaCenaRentiranja = rentiranje.UkupnaCenaRentiranja;
                    existingRentiranje.PlacenoR = rentiranje.PlacenoR;
                    existingRentiranje.DatumPlacanja = rentiranje.DatumPlacanja;
                    existingRentiranje.PristupniKod = rentiranje.PristupniKod;
                    /*
                    existingRentiranje.KorisnikId = rentiranje.KorisnikId;
                    existingRentiranje.Korisnik = rentiranje.Korisnik;
                    existingRentiranje.ZaposleniId = rentiranje.ZaposleniId;
                    existingRentiranje.Zaposleni = rentiranje.Zaposleni;
                    existingRentiranje.AutomobilId = rentiranje.AutomobilId;
                    existingRentiranje.Automobil = rentiranje.Automobil; */
    
                    _ = context.Entry(existingRentiranje).State;

                    int affectedRows = await context.SaveChangesAsync(); // Check return value

                    if (affectedRows > 0)
                    {
                        return mapper.Map<Rentiranje>(existingRentiranje);
                    }
                    else
                    {
                        // Log or throw an exception indicating that no changes were saved
                        throw new Exception("No changes were saved to the database.");
                    }

                }

                else
                {
                    throw new KeyNotFoundException($"Rentiranje with ID {rentiranje.RentiranjeId} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /*
        public async Task UpdateRentiranje(Rentiranje rentiranje)
        {
            await context.SaveChangesAsync();
        }
        */
    
        public async Task DeleteRentiranje(Guid RentiranjeId)
        {
            var rentiranje = await GetRentiranjeById(RentiranjeId);
            context.Rentiranjes.Remove(rentiranje);
            await context.SaveChangesAsync();
        }

        public async Task<List<Rentiranje>> GetRentiranjeByKorisnikId(Guid KorisnikId)
        {
            var rentiranja = await context.Rentiranjes.Include(k => k.Korisnik)
                                                                        .Include(z => z.Zaposleni)
                                                                        .Include(a => a.Automobil)
                                                                        .Where(k => k.KorisnikId == KorisnikId)
                                                                        .ToListAsync();

            foreach (Rentiranje rentiranje in rentiranja)
            {
                Console.WriteLine(rentiranje);
            }
            return rentiranja;
        }

        public async Task<List<Rentiranje>> GetRentiranjeByAutomobilId(Guid AutomobilId)
        {
            var rentiranja = await context.Rentiranjes.Include(k => k.Korisnik)
                                                                        .Include(z => z.Zaposleni)
                                                                        .Include(a => a.Automobil)
                                                                        .Where(k => k.AutomobilId == AutomobilId)
                                                                        .ToListAsync();

            foreach (Rentiranje rentiranje in rentiranja)
            {
                Console.WriteLine(rentiranje);
            }
            return rentiranja;
        }
    }
}
