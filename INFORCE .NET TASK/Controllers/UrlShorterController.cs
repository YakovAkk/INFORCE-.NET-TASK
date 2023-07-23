using INFORCE_.NET_TASK.Services.Model.DTO;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace INFORCE_.NET_TASK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShorterController : ControllerBase
    {
        private readonly IUrlShorterService _urlShorterService;
        public UrlShorterController(IUrlShorterService urlShorterService)
        {
            _urlShorterService = urlShorterService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateShortenUrl([FromBody] UrlInputModel inputModel)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return BadRequest();
            await _urlShorterService.CreateShortUrlAsync(inputModel, userId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUrls()
        {
            var result = await _urlShorterService.GetAllAsync(HttpContext);
            return Ok(result);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _urlShorterService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("for-user/{urlCode}"), Authorize]
        public async Task<IActionResult> DeleteUsersUrls([FromRoute] string urlCode)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return BadRequest();

            await _urlShorterService.DeleteUrlAsync(userId, urlCode);
            return Ok();
        }
    }
}
