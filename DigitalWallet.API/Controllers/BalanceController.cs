using DigitalWallet.API.Common;
using DigitalWallet.API.Features.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BalanceController : ControllerBase
    {
        private readonly IGetBalance _getBalanceService;
        public BalanceController(IGetBalance getBalanceService)
        {
            _getBalanceService = getBalanceService;
        }
        [HttpGet("Get Balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _getBalanceService.GetBalanceAsync(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(BaseResponse<decimal>.Fail(result.Error));
            }
            return Ok(BaseResponse<decimal>.Ok(result.Value));
        }
    }
}
