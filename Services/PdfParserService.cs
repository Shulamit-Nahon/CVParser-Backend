using CVParserAPI.Models;
using CVParserAPI.Services.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using OpenNLP.Tools.NameFind;


namespace CVParserAPI.Services
{
    public class PdfParserService : IPdfParserService
    {
        public async Task<ApplicantData> ParsePdfAsync(byte[] pdfBytes)
        {
            var applicantData = new ApplicantData();

            using (var reader = new PdfReader(pdfBytes))
            {
                var text = "";
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, i);
                }

                applicantData.RawData = text;
                applicantData.PersonalId = ExtractPersonalId(text);
                applicantData.Name = ExtractName(text);
                applicantData.FamilyName = ExtractFamilyName(text);
                applicantData.MobilePhone = ExtractMobilePhone(text);
                applicantData.Email = ExtractEmail(text);
                applicantData.LinkedInUrl = ExtractLinkedInUrl(text);
            }

            return applicantData;
        }
        private string ExtractPersonalId(string text)
        {
            var match = Regex.Match(text, @"\b\d{9}\b");
            return match.Success ? match.Value : null;

        }

        private string ExtractName(string text)
        {
            var firstLine = text.Split('\n').FirstOrDefault();
            if (!string.IsNullOrEmpty(firstLine))
            {
                var words = firstLine.Split(' ');
                if (words.Length > 0)
                {
                    return words[0];
                }
            }
            return "";
        }

        private string ExtractFamilyName(string text)
        {
            var firstLine = text.Split('\n').FirstOrDefault();
            if (!string.IsNullOrEmpty(firstLine))
            {
                var words = firstLine.Split(' ');
                if (words.Length > 1)
                {
                    return words[1];
                }
            }
            return "";
        }
        private string ExtractMobilePhone(string text)
        {
            var match = Regex.Match(text, @"\b(0\d{2}-?\d{3}-?\d{4}|\+972\d{9})\b");
            return match.Success ? match.Value : null;
        }

        private string ExtractEmail(string text)
        {
            var match = Regex.Match(text, @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b");
            return match.Success ? match.Value : null;
        }

        private string ExtractLinkedInUrl(string text)
        {
            var match = Regex.Match(text, @"https?:\/\/[www\.]*linkedin.com\/in\/[\w\-]+\/?");
            return match.Success ? match.Value : null;
        }

    }
}