using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Text;

namespace FolderView.Services
{
    public interface IPdfService
    {
        Task<string> ExtractTextFromPdfAsync(Stream pdfStream);
    }

    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public async Task<string> ExtractTextFromPdfAsync(Stream pdfStream)
        {
            try
            {
                var textBuilder = new StringBuilder();

                using (var document = PdfDocument.Open(pdfStream))
                {
                    foreach (Page page in document.GetPages())
                    {
                        var pageText = page.Text;
                        textBuilder.AppendLine(pageText);
                        textBuilder.AppendLine(); // Separador entre páginas
                    }
                }

                var extractedText = textBuilder.ToString();

                if (string.IsNullOrWhiteSpace(extractedText))
                {
                    _logger.LogWarning("No se pudo extraer texto del PDF o el PDF está vacío");
                    throw new InvalidOperationException("El PDF no contiene texto extraíble o está vacío");
                }

                _logger.LogInformation($"Texto extraído del PDF: {extractedText.Length} caracteres");

                return await Task.FromResult(extractedText);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al extraer texto del PDF");
                throw;
            }
        }
    }
}
