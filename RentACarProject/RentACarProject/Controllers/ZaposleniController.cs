using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.DTO;
using RentACarProject.Models;
using RentACarProject.Repository.AutomobilRepository;
using RentACarProject.Repository.ZaposleniRepository;

namespace RentACarProject.Controllers
{
    [ApiController]
    [Route("api/zaposleni")]
    [Produces("application/json", "application/xml")]
    public class ZaposleniController : ControllerBase
    {
        private readonly IZaposleniRepository zaposleniRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ZaposleniController(IZaposleniRepository zaposleniRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zaposleniRepository = zaposleniRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<ZaposleniDTO>> GetZaposleni()
        {
            List<Zaposleni> zaposleni = zaposleniRepository.GetZaposleni();
            if (zaposleni == null || zaposleni.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ZaposleniDTO>>(zaposleni));
        }

        [HttpGet("{ZaposleniId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ZaposleniDTO> GetZaposleniById(Guid ZaposleniId)
        {
            var zaposleni = zaposleniRepository.GetZaposleniById(ZaposleniId);

            if (zaposleni == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ZaposleniDTO>(zaposleni));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Zaposleni> CreateUser([FromBody] ZaposleniCreateDTO zaposleni)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Zaposleni mappedZaposleni = mapper.Map<Zaposleni>(zaposleni);
                Zaposleni createdZaposleni = zaposleniRepository.CreateZaposleni(mappedZaposleni);
                zaposleniRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetZaposleni", "Zaposleni", new { ZaposleniId = createdZaposleni.ZaposleniId });

                return Created(location, mapper.Map<Zaposleni>(createdZaposleni));
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{ZaposleniId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteZaposleni(Guid ZaposleniId)
        {
            try
            {
                var zaposleni = zaposleniRepository.GetZaposleniById(ZaposleniId);

                if (zaposleni == null)
                {
                    return NotFound();
                }

                zaposleniRepository.DeleteZaposleni(ZaposleniId);
                zaposleniRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }



        [HttpPut("{ZaposleniId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<ZaposleniDTO> UpdateZaposleni(Guid ZaposleniId, [FromBody] ZaposleniUpdateDTO zaposleni)
        {
            try
            {
                zaposleni.ZaposleniId = ZaposleniId;
                Zaposleni mappedZaposleni = mapper.Map<Zaposleni>(zaposleni);
                var updatedZaposleni = zaposleniRepository.UpdateZaposleni(mappedZaposleni);

                return Ok(mapper.Map<ZaposleniDTO>(updatedZaposleni));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
