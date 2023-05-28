using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.VersionServices.Version1;

namespace WebApplication1.Controllers.VersionControllers.Version1
{
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v1/versioned")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IIntegerService _integerService;

        public VersionController(IIntegerService integerService)
        {
            _integerService = integerService;
        }

        [HttpGet]
        [Authorize]
        [Obsolete("Depreceated")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _integerService.GetRandomInteger());
        }
    }
}
