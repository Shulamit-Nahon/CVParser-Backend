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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicantData>>> GetAllApplicants()
        {
            var applicants = await _mongoDbService.GetAsync();
            return Ok(applicants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicantData>> GetApplicant(string id)
        {
            var applicant = await _mongoDbService.GetAsync(id);

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

}