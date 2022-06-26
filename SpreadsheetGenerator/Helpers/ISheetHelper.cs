namespace SpreadsheetGenerator.Helpers;

public interface ISheetHelper
{
    public byte[] GenerateSpreadsheet<T>(IList<T> obj, string worksheetName);
}
