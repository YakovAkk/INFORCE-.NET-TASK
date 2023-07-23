using INFORCE_.NET_TASK.Services.Model.InputModel;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace INFORCE_.NET_TASK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptionController : ControllerBase
    {
        private readonly IDescriptionService _descriptionService;
        public DescriptionController(IDescriptionService descriptionService)
        {
            _descriptionService = descriptionService;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetDescription([FromBody] DescriptionInputModel inputModel)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return BadRequest();

            await _descriptionService.SetDescriptionAsync(inputModel);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetDescription()
        {
            var result = await _descriptionService.GetDescriptionAsync();
            return Ok(result);
        }
    }
}
