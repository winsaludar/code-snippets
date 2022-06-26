namespace SpreadsheetGenerator.Services;

public interface IReportService
{
    public (bool isSuccesful, string message, byte[]? excelSheetAsByteArray) GenerateProductSalesReport();
}
