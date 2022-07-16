using EmpServiciosPublicas.Aplication.Contracts.Identity;
using EmpServiciosPublicas.Aplication.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAuthServices _authService;

        public AccountController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest authRequest)
        {
            return Ok(await _authService.Login(authRequest));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            return Ok(await _authService.Register(registerRequest));
        }
    }
}
