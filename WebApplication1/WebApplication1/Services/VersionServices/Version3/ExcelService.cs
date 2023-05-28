using ClosedXML.Excel;

namespace WebApplication1.Services.VersionServices.Version3
{
    public class ExcelService : IExcelService
    {
        public async Task<XLWorkbook> GetXLWorkbook()
        {
            return await Task.Run(() =>
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.AddWorksheet("Sample Sheet");
                worksheet.Cell("A1").Value = "Hello World!";
                worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";
                
                return workbook;
            });
        }
    }
}
