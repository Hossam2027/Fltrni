using Fltrni.core.DTO;
using Fltrni.core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fltrni.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService AuthService { get; }

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await AuthService.RegisterAsync(dto);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await AuthService.LoginAsync(dto);
            return Ok(result);
        }
    }
}
