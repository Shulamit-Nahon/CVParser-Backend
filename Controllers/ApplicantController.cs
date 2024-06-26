using CVParserAPI.Models;
using CVParserAPI.Services;
using CVParserAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CVParserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IPdfParserService _pdfParserService;

        public ApplicantController(MongoDbService mongoDbService, IPdfParserService pdfParserService)
        {
            _mongoDbService = mongoDbService;
            _pdfParserService = pdfParserService;
        }


        [HttpPost]
        public async Task<IActionResult> UploadCV(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("File must be a PDF");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var pdfBytes = memoryStream.ToArray();

            try
            {
                var applicantData = await _pdfParserService.ParsePdfAsync(pdfBytes);
                applicantData.OriginalFileName = file.FileName;
                await _mongoDbService.CreateAsync(applicantData);

                return CreatedAtAction(nameof(GetApplicant), new { id = applicantData.Id }, applicantData);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the CV");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApplicantData>> GetApplicant(string name)
        {
            var applicant = await _mongoDbService.GetAsync(name);

            if (applicant == null)
            {
                return NotFound();
            }

            return applicant;
        }

        [HttpDelete("name/{name}")]
        public async Task<IActionResult> DeleteApplicant(string name)
        {
            var applicant = await _mongoDbService.GetAsync(name);

            if (applicant == null)
            {
                return NotFound();
            }

            await _mongoDbService.RemoveAsync(name);

            return NoContent();
        }
    }


    /*
    [HttpGet("{id:length(24)}/download")]
    public async Task<IActionResult> DownloadCV(string id)
    {
        var applicant = await _mongoDbService.GetAsync(id);

        if (applicant == null)
        {
            return NotFound();
        }

        // Assuming the original PDF is stored in the database or file system
        // You may need to adjust this based on how you're storing the original PDF
        byte[] pdfBytes = await _mongoDbService.GetOriginalPdfAsync(id);

        if (pdfBytes == null || pdfBytes.Length == 0)
        {
            return NotFound("Original PDF not found");
        }

        string fileName = applicant.OriginalFileName ?? $"{applicant.Name}_{applicant.FamilyName}_CV.pdf";
        return File(pdfBytes, "application/pdf", fileName);
    }*/

}