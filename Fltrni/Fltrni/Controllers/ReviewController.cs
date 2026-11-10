using Fltrni.core.DTO;
using Fltrni.core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fltrni.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("provider/{id}")]
        public async Task<IActionResult> Create(CreateReviewDto dto)
        {
            // Get the customer ID from the authenticated user
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized("User is not authenticated.");
            }
            var result = await _reviewService.CreateAsync(dto, customerId);
            if (result != "Review created successfully")
            {
                return BadRequest(result);
            }
            return Ok(dto);
        }
        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetByProvider(int providerId)
        {
            var reviews = await _reviewService.GetByProviderAsync(providerId);
            return Ok(reviews);
        }
    }
}
