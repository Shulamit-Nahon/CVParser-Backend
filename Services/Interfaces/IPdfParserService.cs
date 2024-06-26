using CVParserAPI.Models;

namespace CVParserAPI.Services.Interfaces
{
    public interface IPdfParserService
    {
        Task<ApplicantData> ParsePdfAsync(byte[] pdfBytes);
    }
}