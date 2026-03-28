using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using premium_calculator_api.Models;
using premium_calculator_api.Services;

namespace premium_calculator_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremiumController : ControllerBase
    {
        private readonly IPremiumService _service;

        public PremiumController(IPremiumService service)
        {
            _service = service;
        }

        [HttpGet("occupations")]
        public IActionResult GetOccupations()
        {
            return Ok(_service.GetOccupations());
        }

        [HttpPost("calculate")]
        public IActionResult CalculatePremium(PremiumRequest request)
        {
            var premium = _service.CalculatePremium(request);
            return Ok(new PremiumResponse { MonthlyPremium = premium });
        }
    }

}
