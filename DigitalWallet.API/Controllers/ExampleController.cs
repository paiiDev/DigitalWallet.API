using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        [Authorize]
        [HttpGet("Balance")]
        public IActionResult GetBalance()
        {
            return Ok("You are authenticated.");
        }

    }
}
