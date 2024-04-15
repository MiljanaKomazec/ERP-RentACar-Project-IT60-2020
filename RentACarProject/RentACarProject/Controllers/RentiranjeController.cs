﻿using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.DTO;
using RentACarProject.Models;
using RentACarProject.Repository.AutomobilRepository;
using RentACarProject.Repository.KorisnikRepository;
using RentACarProject.Repository.RentiranjeRepository;

namespace RentACarProject.Controllers
{
    [ApiController]
    [Route("api/rentiranje")]
    [Produces("application/json", "application/xml")]
    public class RentiranjeController : ControllerBase
    {
        private readonly IRentiranjeRepository rentRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public RentiranjeController(IRentiranjeRepository rentRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.rentRepository = rentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<Rentiranje>>> GetRentiranje()
        {
            var rentiranje = await rentRepository.GetRentiranje();
            if (rentiranje == null || rentiranje.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<Rentiranje>>(rentiranje));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{RentiranjeId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<RentiranjeDTO>> GetRentiranjeById(Guid RentiranjeId)
        {
            var rentiranje = await rentRepository.GetRentiranjeById(RentiranjeId);

            if (rentiranje == null)
            {
                return NotFound();
            }
            
            return Ok(mapper.Map<RentiranjeDTO>(rentiranje));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<RentiranjeDTO>> CreateRentiranje([FromBody] RentiranjeCreateDTO rentiranje)
        {


            try
            {

                Rentiranje rentiranjeModel = mapper.Map<Rentiranje>(rentiranje);
                Rentiranje confirmation = await rentRepository.CreateRentiranje(rentiranjeModel);
                await rentRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetRentiranje", "Rentiranje", new { RentiranjeId = confirmation.RentiranjeId });
           
                return Ok(mapper.Map<RentiranjeDTO>(confirmation));


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }


        [HttpPut("{RentiranjeId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<RentiranjeDTO>> UpdateRentiranje(Guid RentiranjeId, [FromBody] RentiranjeUpdateDTO rentiranje)
        {
            try
            {
                rentiranje.RentiranjeId = RentiranjeId;
                Rentiranje mappedRentiranje = mapper.Map<Rentiranje>(rentiranje);
                var updatedRentiranje = await rentRepository.UpdateRentiranje(mappedRentiranje);


                return Ok(mapper.Map<RentiranjeDTO>(updatedRentiranje));

                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /*

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<Rentiranje>> UpdateRentiranje(Rentiranje rentiranje)
        {
            try
            {

                var oldRentiranje = await rentRepository.GetRentiranjeById(rentiranje.RentiranjeId);
                if (oldRentiranje == null)
                {
                    return NotFound();
                }

                Rentiranje rentiranjeEntity = mapper.Map<Rentiranje>(rentiranje);

                mapper.Map(rentiranjeEntity, oldRentiranje);

                await rentRepository.SaveChanges();
                return Ok(mapper.Map<Rentiranje>(oldRentiranje));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error:" + ex.Message);
            }
        }
        */

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{RentiranjeId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> DeleteRentiranje(Guid RentiranjeId)
        {
            try
            {
                var rentiranje = await rentRepository.GetRentiranjeById(RentiranjeId);

                if (rentiranje == null)
                {
                    return NotFound();
                }

                await rentRepository.DeleteRentiranje(RentiranjeId);
                await rentRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("korisnik/{KorisnikId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<RentiranjeDTO>>> GetRentiranjeByKorisnikId(Guid KorisnikId)
        {
            var rentiranje = await rentRepository.GetRentiranjeByKorisnikId(KorisnikId);

            if (rentiranje == null || rentiranje.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<RentiranjeDTO>>(rentiranje));
        }
    }
}
