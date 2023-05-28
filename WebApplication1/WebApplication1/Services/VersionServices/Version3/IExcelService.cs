using ClosedXML.Excel;

namespace WebApplication1.Services.VersionServices.Version3
{
    public interface IExcelService
    {
        Task<XLWorkbook> GetXLWorkbook();
    }
}
