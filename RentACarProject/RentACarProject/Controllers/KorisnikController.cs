﻿using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.DTO;
using RentACarProject.Models;
using RentACarProject.Repository.KorisnikRepository;
using RentACarProject.Repository.ZaposleniRepository;


namespace RentACarProject.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KorisnikController(IKorisnikRepository korisnikRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<KorisnikDTO>> GetKorisnik()
        {
            List<Korisnik> korisnici = korisnikRepository.GetKorisnik();
            if (korisnici == null || korisnici.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<KorisnikDTO>>(korisnici));
        }

        [HttpGet("{KorisnikId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<KorisnikDTO> GetKorisnikById(Guid KorisnikId)
        {
            var korisnik = korisnikRepository.GetKorisnikById(KorisnikId);

            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<KorisnikDTO>(korisnik));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Korisnik> CreateKorisnik([FromBody] KorisnikCreateDTO korisnik)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Korisnik mappedKorisnik = mapper.Map<Korisnik>(korisnik);
                Korisnik createdKorisnik = korisnikRepository.CreateKorisnik(mappedKorisnik);
                korisnikRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetKorisnik", "Korisnik", new { KorisnikId = createdKorisnik.KorisnikId });

                return Created(location, mapper.Map<Korisnik>(createdKorisnik));
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{KorisnikId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKorisnik(Guid KorisnikId)
        {
            try
            {
                var korisnik = korisnikRepository.GetKorisnikById(KorisnikId);

                if (korisnik == null)
                {
                    return NotFound();
                }

                korisnikRepository.DeleteKorisnik(KorisnikId);
                korisnikRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut("{KorisnikId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<KorisnikDTO> UpdateKorisnik(Guid KorisnikId, [FromBody] KorisnikUpdateDTO korisnik)
        {
            try
            {
                korisnik.KorisnikId = KorisnikId;
                Korisnik mappedKorisnik = mapper.Map<Korisnik>(korisnik);
                var updatedKorisnik = korisnikRepository.UpdateKorisnik(mappedKorisnik);

                return Ok(mapper.Map<KorisnikDTO>(updatedKorisnik));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
