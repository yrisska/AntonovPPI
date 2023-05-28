using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.VersionServices.Version3;

namespace WebApplication1.Controllers.VersionControllers.Version3
{
    [ApiExplorerSettings(GroupName = "v3")]
    [ApiVersion("3.0")]
    [Route("api/v3/versioned")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public VersionController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            using MemoryStream stream = new MemoryStream();

            var workbook = await _excelService.GetXLWorkbook();

            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(
                fileContents: stream.ToArray(),
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",

                fileDownloadName: "TestSheet.xlsx"
            );
        }
    }
}
