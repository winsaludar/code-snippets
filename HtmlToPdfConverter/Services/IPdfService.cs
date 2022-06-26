namespace HtmlToPdfConverter.Services;

public interface IPdfService
{
    public Task<(bool isSuccesful, string message, byte[]? pdfAsByteArray)> ConvertWebpageToPdf(string url);
}
