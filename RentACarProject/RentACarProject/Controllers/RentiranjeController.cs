using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.DTO;
using RentACarProject.Models;
using RentACarProject.Repository.AutomobilRepository;
using RentACarProject.Repository.KorisnikRepository;
using RentACarProject.Repository.RentiranjeRepository;
using Stripe;
using Stripe.Checkout;

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
            StripeConfiguration.ApiKey = "sk_test_51PG5DH01Y3CG6EWFDJrXdXAEzbHgSgrdKYDisNZ8Kb9mRcMc6Y72jAQfmWIr3VrXTWhv8C4TrxsizycUMiJFbq0Q0072nNXpyt";
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<PaginatedRentiranjeDTO>> GetRentiranje([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var response = await rentRepository.GetRentiranje(page, pageSize);
            if (response.Rentiranja == null || response.Rentiranja.Count == 0)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin, Korisnik")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<RentiranjeDTO>> CreateRentiranje([FromBody] RentiranjeCreateDTO rentiranje)
        {


            try
            {
                
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(rentiranje.UkupnaCenaRentiranja) *100, 
                    Currency = "eur",
                    Description = "Rentiranje automobila",
                    Source = rentiranje.StripeToken,
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.Status != "succeeded")
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Payment Error");
                }

                Rentiranje rentiranjeModel = mapper.Map<Rentiranje>(rentiranje);
                rentiranjeModel.StripeChargeId = charge.Id;
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

        [Authorize(Roles = "Admin, Korisnik")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Korisnik, Zaposleni")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("korisnik/{KorisnikId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<Rentiranje>>> GetRentiranjeByKorisnikId(Guid KorisnikId)
        {
            var rentiranje = await rentRepository.GetRentiranjeByKorisnikId(KorisnikId);

            if (rentiranje == null || rentiranje.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<Rentiranje>>(rentiranje));
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("automobil/{AutomobilId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<RentiranjeDTO>>> GetRentiranjeByAutomobilId(Guid AutomobilId)
        {
            var rentiranje = await rentRepository.GetRentiranjeByAutomobilId(AutomobilId);

            if (rentiranje == null || rentiranje.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<RentiranjeDTO>>(rentiranje));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("dostupnost/{AutomobilId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<RentiranjeDostupnostDTO>>> GetDostupnostRentiranjeByAutomobilId(Guid AutomobilId)
        {
            var rentiranje = await rentRepository.GetRentiranjeByAutomobilId(AutomobilId);

            if (rentiranje == null || rentiranje.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<RentiranjeDostupnostDTO>>(rentiranje));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];
            var webhookSecret = "whsec_79kpM9PhE24odBKo0GxH7yR3gEXXic08"; 

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signatureHeader,
                    webhookSecret
                );

                Console.WriteLine($"Received Stripe event: {stripeEvent.Type}");

                if (stripeEvent.Type == Events.ChargeSucceeded)
                {
                    var charge = stripeEvent.Data.Object as Charge;
                    Console.WriteLine($"Charge succeeded for charge ID: {charge.Id}");

                    var rentiranje = await rentRepository.GetRentiranjeByStripeChargeId(charge.Id);
                    if (rentiranje != null)
                    {
                        rentiranje.PlacenoR = "Da";
                        rentiranje.DatumPlacanja = DateTime.UtcNow;
                        var updatedRentiranje = await rentRepository.UpdateRentiranje(rentiranje);

                        Console.WriteLine($"Updated rentiranje status to 'Paid' for charge ID: {charge.Id}");
                    }
                    else
                    {
                        Console.WriteLine($"No rentiranje found for charge ID: {charge.Id}");
                    }
                }
                else
                {
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                }

                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Stripe exception: {e.Message}");
                return BadRequest(new { error = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine($"General exception: {e.Message}");
                return BadRequest(new { error = e.Message });
            }
        }



    }
}
