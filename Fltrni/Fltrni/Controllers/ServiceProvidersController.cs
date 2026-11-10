using Fltrni.core.DTO;
using Fltrni.core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fltrni.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly IServiceProviderService _serviceProviderService;

        public ServiceProvidersController(IServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ServiceProviderDto>>>GetServiceProviders([FromQuery] ProviderSpecParams specParams)
        {
            var result = await _serviceProviderService.GetallAsync(specParams);
            return Ok(result);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPending()
        {
            var parameters = new ProviderSpecParams
            {
                PendingOnly = true
            };

            var providers = await _serviceProviderService.GetallAsync(parameters);

            return Ok(providers);
        }
        [HttpPost]
        [Authorize(Roles = "Customer,Provider,Admin")]
        public async Task<IActionResult> Create(CreateServiceProviderDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            await _serviceProviderService.CreateServiceProviderAsync(dto, userId);

            return Ok(new { message = "Service provider created successfully" });
        }
        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _serviceProviderService.ApproveAsync(id);

            return Ok(result);
        }
        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            var result = await _serviceProviderService.RejectAsync(id);

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceProviderService.DeleteAsync(id);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var provider = await _serviceProviderService.GetserviceProviderByIdAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            return Ok(provider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateServiceProviderDto dto)
        {
            await _serviceProviderService.UpdateAsync(id, dto);
            return Ok(dto);
        }

        [Authorize(Roles = "Customer,Provider,Admin")]
        [HttpGet("my-status")]
        public async Task<ActionResult> GetMyStatus()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();

            var provider = await _serviceProviderService.GetMyStatusAsync(userId);

            if (provider is null)
                return NotFound("You haven't submitted a provider application.");

            return Ok(new
            {
                provider.Status,
                provider.Id
            });
        }


        [HttpGet("areas")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetAreas(
        [FromQuery] int? categoryId,
        [FromQuery] string? governorate)
        {
            var areas = await _serviceProviderService.GetAreasAsync(categoryId, governorate);
            return Ok(areas);
        }
    }

}