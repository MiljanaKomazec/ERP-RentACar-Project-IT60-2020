
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.DTO;
using RentACarProject.Models;
using RentACarProject.Repository.AutomobilRepository;

namespace RentACarProject.Controllers
{
    
    [ApiController]
    [Route("api/automobil")]
    [Produces("application/json", "application/xml")]
    public class AutomobilController : ControllerBase
    {
        private readonly IAutomobilRepository autoRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public AutomobilController(IAutomobilRepository autoRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.autoRepository = autoRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<Automobil>> GetAutomobil()
        {
            List<Automobil> auto = autoRepository.GetAutomobil();
            if (auto == null || auto.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<Automobil>>(auto));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{AutomobilId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<AutomobilDTO> GetAutomobilById(Guid AutomobilId)
        {
            var auto = autoRepository.GetAutomobilById(AutomobilId);

            if (auto == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AutomobilDTO>(auto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<AutomobilDTO> CreateAutomobil([FromBody] AutomobilDTO auto)
        {


            try
            {

                Automobil autoModel = mapper.Map<Automobil>(auto);
                Automobil confirmation = autoRepository.CreateAutomobil(autoModel);

                autoRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetAutomobil", "Automobil", new { automobilId = confirmation.AutomobilId });
                return Ok(mapper.Map<AutomobilDTO>(confirmation));


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [Authorize(Roles = "Admin, Zaposleni")]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<AutomobilDTO> UpdateAutomobil(Automobil auto)
        {
            try
            {

                var oldAuto = autoRepository.GetAutomobilById(auto.AutomobilId);
                if (oldAuto == null)
                {
                    return NotFound();
                }
                Automobil autoEntity = mapper.Map<Automobil>(auto);

                mapper.Map(autoEntity, oldAuto);

                autoRepository.SaveChanges();
                return Ok(mapper.Map<AutomobilDTO>(oldAuto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{AutomobilId}")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeleteAutomobil(Guid AutomobilId)
        {
            try
            {
                var auto = autoRepository.GetAutomobilById(AutomobilId);

                if (auto == null)
                {
                    return NotFound();
                }

                autoRepository.DeleteAutomobil(AutomobilId);
                autoRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpGet("tipMenjaca/{TipM}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<AutomobilDTO>> GetAutomobilByTipMenjaca(string TipM)
        {
            List<Automobil> auto = autoRepository.GetAutomobilByTipMenjaca(TipM);
            if (auto == null || auto.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<AutomobilDTO>>(auto));
        }

        [HttpGet("tipAutomobila/{TipA}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<AutomobilDTO>> GetAutomobilByTipAutomobila(string TipA)
        {
            List<Automobil> auto = autoRepository.GetAutomobilByTipA(TipA);
            if (auto == null || auto.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<AutomobilDTO>>(auto));
        }

        [HttpGet("markaAutomobila/{MarkaA}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<AutomobilDTO>> GetAutomobilByMarkaAutomobila(string MarkaA)
        {
            List<Automobil> auto = autoRepository.GetAutomobilByMarka(MarkaA);
            if (auto == null || auto.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<AutomobilDTO>>(auto));
        }

        [HttpGet("tipMiA/{TipM}/{TipA}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<AutomobilDTO>> GetAutomobilByTipMiA(string TipM, string TipA)
        {
            List<Automobil> auto = autoRepository.GetAutomobilByTipMiA(TipM, TipA);
            if (auto == null || auto.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<AutomobilDTO>>(auto));
        }



    }

}
