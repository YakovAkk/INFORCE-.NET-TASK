using INFORCE_.NET_TASK.Services.Model.InputModel;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace INFORCE_.NET_TASK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginInputModel inputModel)
        {
            var result = await _authService.LoginAsync(inputModel);
            return Ok(result);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationInputModel inputModel)
        {
            var result = await _authService.RegisterAsync(inputModel);
            return Ok(result);
        }
    }
}
