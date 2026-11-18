using CustomerManagement.Api.DTOs;
using CustomerManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponseDto> Login(
            [FromBody] LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);

            if(result == null)
            {
                return Unauthorized();
            }

            return Ok(result);

        }
       
    }
}
