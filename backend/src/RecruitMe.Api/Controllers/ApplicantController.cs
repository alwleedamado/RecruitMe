using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;

namespace RecruitMe.Api.Controllers
{
    [Route("api/applicants")]
    [ApiController]
    public class ApplicantController(IApplicantService applicantService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var applicants = await applicantService.GetAllApplicantsAsync();
            return Ok(applicants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var applicant = await applicantService.GetApplicantByIdAsync(id);
            if (applicant == null) {
                return NotFound();
            }
            return Ok(applicant);
        }

        [HttpPost("{id}/experiences")]
        public async Task<IActionResult> AddWorkExperience(int id, [FromBody] WorkExperienceDto workExperienceDto)
        {
            await applicantService.AddExperienceAsync(id, workExperienceDto);
            return Ok();
        }
    }
}
