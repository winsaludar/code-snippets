using HtmlToPdfConverter.Helpers;

namespace HtmlToPdfConverter.Services;

public class PdfService : IPdfService
{
    private readonly HttpClient _httpClient;
    private readonly IPdfHelper _pdfHelper;

    // Injected through dependency injection
    public PdfService(HttpClient httpClient, IPdfHelper pdfHelper)
    {
        _httpClient = httpClient;
        _pdfHelper = pdfHelper;
    }

    public async Task<(bool isSuccesful, string message, byte[]? pdfAsByteArray)> ConvertWebpageToPdf(string url)
    {
        try
        {
            // Fetch url
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string html = await response.Content.ReadAsStringAsync();

            byte[] pdf = _pdfHelper.ConvertHtmlToPdf(html);
            return (true, "Success", pdf);
        }
        catch (Exception)
        {
            return (false, "Error!", null);
        }
    }
}
