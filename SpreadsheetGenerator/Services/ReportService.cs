using SpreadsheetGenerator.Helpers;
using SpreadsheetGenerator.Models;

namespace SpreadsheetGenerator.Services;

public class ReportService : IReportService
{
    private readonly ISheetHelper _sheetHelper;

    // Injected through dependency injection
    public ReportService(ISheetHelper sheetHelper) => _sheetHelper = sheetHelper;

    public (bool isSuccesful, string message, byte[]? excelSheetAsByteArray) GenerateProductSalesReport()
    {
        try
        {
            // Get products from other service
            List<Product> products = new();
            
            byte[] excelSheet = _sheetHelper.GenerateSpreadsheet(products, "Products Monthly Report");
            return (true, "Success", excelSheet);
        }
        catch (Exception)
        {
            return (false, "Error!", null);
        }
    }
}
