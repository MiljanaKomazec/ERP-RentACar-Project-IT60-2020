
using AutoMapper;
using RentACarProject.Data;
using RentACarProject.Models;



namespace RentACarProject.Repository.AutomobilRepository
{ 
    public class AutomobilRepository : IAutomobilRepository
    {
        private readonly RentACarContext context;
        private readonly IMapper mapper;

        public AutomobilRepository(RentACarContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Automobil> GetAutomobil()
        {
            try{
                
                
                var obj = context.Automobils
                    .ToList();
                if (obj != null)
                {
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

        public Automobil GetAutomobilById(Guid AutomobilId)
        {
            return context.Automobils.FirstOrDefault(e => e.AutomobilId == AutomobilId);
        }

        public Automobil CreateAutomobil(Automobil automobil)
        {
            automobil.AutomobilId = Guid.NewGuid();
            var createdEntity = context.Add(automobil);
            return mapper.Map<Automobil>(createdEntity.Entity);
        }

        public void DeleteAutomobil(Guid AutomobilId)
        {
            var auto = GetAutomobilById(AutomobilId);
            context.Remove(auto);
        }

        public void UpdateAutomobil(Automobil automobil)
        {
            throw new NotImplementedException();
        }

        public List<Automobil> GetAutomobilByTipMenjaca(string TipM)
        {
            var automobili = context.Automobils.Where(m => m.TipMenjaca == TipM)
                                                                       .ToList();
            foreach (Automobil automobil in automobili)
            {
                Console.WriteLine(automobil);
            }
            return automobili;
        }

        public List<Automobil> GetAutomobilByTipA(string TipA)
        {
            var automobili = context.Automobils.Where(m => m.TipAutomobila == TipA)
                                                                       .ToList();
            foreach (Automobil automobil in automobili)
            {
                Console.WriteLine(automobil);
            }
            return automobili;
        }

        public List<Automobil> GetAutomobilByMarka(string MarkaA)
        {
            var automobili = context.Automobils.Where(m => m.MarkaAutomobila == MarkaA)
                                                                       .ToList();
            foreach (Automobil automobil in automobili)
            {
                Console.WriteLine(automobil);
            }
            return automobili;
        }

        public List<Automobil> GetAutomobilByTipMiA(string TipM, string TipA)
        {
            var automobili = context.Automobils.Where(m => m.TipMenjaca == TipM)
                                               .Where(a => a.TipAutomobila == TipA)
                                                                       .ToList();
            foreach (Automobil automobil in automobili)
            {
                Console.WriteLine(automobil);
            }
            return automobili;
        }
    }
}

