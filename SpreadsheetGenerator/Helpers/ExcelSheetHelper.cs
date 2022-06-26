using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel;
using System.Reflection;

namespace SpreadsheetGenerator.Helpers;

public class ExcelSheetHelper : ISheetHelper
{
    public byte[] GenerateSpreadsheet<T>(IList<T> obj, string worksheetName)
    {
        using MemoryStream stream = new MemoryStream();
        using SpreadsheetDocument doc = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
        // Add a workbookpart in the document
        WorkbookPart workbookPart = doc.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();

        // Add a worksheetpart in the workbookpart
        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet(new SheetData());

        // Add sheets to the workbook
        if (doc.WorkbookPart == null)
            throw new NullReferenceException("WorkbookPart is null");
        Sheets sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

        // Append a new worksheet and associate it with the workbook
        Sheet sheet = new()
        {
            Id = doc.WorkbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = worksheetName
        };
        sheets.Append(sheet);

        // Get the sheetData cell table
        SheetData? sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
        if (sheetData == null)
            throw new NullReferenceException("SheetData is null");

        // Return empty excel file if list is empty
        if (obj.Count <= 0)
        {
            // Close the document
            doc.Close();

            // Reset the stream position  to the start of the stream
            stream.Seek(0, SeekOrigin.Begin);

            return stream.ToArray();
        }

        // Add data
        PropertyInfo[] objProperties = obj[0]!.GetType().GetProperties();
        int objPropertiesCount = objProperties.Length;
        for (int i = 0; i < obj.Count; i++)
        {
            // Create header row
            Row headerRow = new Row();
            if (i == 0)
                sheetData.Append(headerRow);

            Row row = new Row();
            sheetData.Append(row);

            for (int j = 0; j < objPropertiesCount; j++)
            {
                string propertyName = GetDisplayName(objProperties[j]);

                // If property name is blank, skip the column
                if (string.IsNullOrEmpty(propertyName))
                    continue;

                // Save property name as header
                if (i == 0)
                {

                    Cell headerCell = new Cell();
                    headerCell.CellValue = new CellValue(propertyName);
                    headerCell.DataType = new EnumValue<CellValues>(CellValues.String);
                    headerRow.Append(headerCell);
                }

                string? propertyValue = objProperties[j].GetValue(obj[i])?.ToString();
                Cell newCell = new();
                newCell.CellValue = new CellValue(propertyValue ?? "");
                newCell.DataType = new EnumValue<CellValues>(CellValues.String);

                row.Append(newCell);
            }
        }

        // Close the document
        doc.Close();

        // Reset the stream position  to the start of the stream
        stream.Seek(0, SeekOrigin.Begin);

        return stream.ToArray();
    }

    private static string GetDisplayName(PropertyInfo property)
    {
        var atts = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);

        if (atts.Length == 0 || atts[0] == null)
            return "";

        return ((DisplayNameAttribute)atts[0]).DisplayName;
    }
}