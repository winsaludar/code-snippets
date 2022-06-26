namespace HtmlToPdfConverter.Helpers;

public interface IPdfHelper
{
    public byte[] ConvertHtmlToPdf(string html, string? imageBaseUrl = null);
}
