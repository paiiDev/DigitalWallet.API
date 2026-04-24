using DigitalWallet.API.Common;
using DigitalWallet.API.DTOs.Transactions;
using DigitalWallet.API.Features.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(TransactionRequestDto request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _transactionService.TransferAsync(userId, request);
            if (!result.IsSuccess)
            {
                return BadRequest(BaseResponse<string>.Fail(result.Error));
            }

            return Ok(BaseResponse<string>.Ok(result.Value));
        }

        [HttpGet("Get Transaction")]
        public async Task<IActionResult> GetTransactionHistory()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _transactionService.GetTransactionsAsync(userId);
            if (!result.IsSuccess) 
            {
                return BadRequest(BaseResponse<GetTransactionsResponseDto>.Fail(result.Error));
            }

            return Ok(BaseResponse<List<GetTransactionsResponseDto>>.Ok(result.Value));


        }
    }
}
