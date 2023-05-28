using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.VersionServices.Version2;

namespace WebApplication1.Controllers.VersionControllers.Version2
{
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiVersion("2.0")]
    [Route("api/v2/versioned")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ITextService _textService;

        public VersionController(ITextService textService)
        {
            _textService = textService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _textService.GetLoremText());
        }
    }
}
