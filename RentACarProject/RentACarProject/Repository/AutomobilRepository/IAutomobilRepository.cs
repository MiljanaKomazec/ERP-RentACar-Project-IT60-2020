

using RentACarProject.Models;

namespace RentACarProject.Repository.AutomobilRepository
{ 
    public interface IAutomobilRepository
    {
        List<Automobil> GetAutomobil();
        Automobil GetAutomobilById(Guid AutomobilId);
        Automobil CreateAutomobil(Automobil automobil);
        void UpdateAutomobil(Automobil automobil);
        void DeleteAutomobil(Guid AutomobilId);

        List<Automobil> GetAutomobilByTipMenjaca(string TipM);
        List<Automobil> GetAutomobilByTipA(string TipA);
        List<Automobil> GetAutomobilByMarka(string MarkaA);
        List<Automobil> GetAutomobilByTipMiA(string TipM, string TipA);

        bool SaveChanges();
    } 
}
