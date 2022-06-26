using SelectPdf;

namespace HtmlToPdfConverter.Helpers;

public class SelectPdfHelper : IPdfHelper
{
    const int MAX_PAGE_LOAD_TIME_SECONDS = 300; // 5 mins

    public byte[] ConvertHtmlToPdf(string html, string? imageBaseUrl = null)
    {
        HtmlToPdf converter = new();
        converter.Options.MaxPageLoadTime = MAX_PAGE_LOAD_TIME_SECONDS;
        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;

        PdfDocument doc = !string.IsNullOrEmpty(imageBaseUrl) 
            ? converter.ConvertHtmlString(html, imageBaseUrl) 
            : converter.ConvertHtmlString(html);

        byte[] result = doc.Save();
        doc.Close();

        return result;
    }
}
