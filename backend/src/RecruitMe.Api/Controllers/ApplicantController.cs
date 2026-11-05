using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Infrastructure.Identity;

namespace RecruitMe.Api.Controllers
{
    [Route("api/applicants")]
    [Authorize]
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

        [HttpPost("experiences")]
        [Authorize(Roles.Applicant)]
        public async Task<IActionResult> AddWorkExperience([FromBody] WorkExperienceDto workExperienceDto, CancellationToken ct)
        {
            await applicantService.AddExperienceAsync(workExperienceDto, ct);
            return Ok();
        }

        [HttpPut("experiences/{id:int}")]
        [Authorize(Roles.Applicant)]
        public async Task<IActionResult> UpdateWorkExperience(int id, [FromBody] UpdateExperienceRequest workExperienceDto, CancellationToken ct)
        {
            if (id != workExperienceDto.Id)
                return BadRequest();
            await applicantService.UpdateExperienceAsync(workExperienceDto, ct);
            return Ok();
        }

        [HttpDelete("{id}/experiences")]
        [Authorize(Roles.Applicant)]
        public async Task<IActionResult> DeleteWorkExperience(int id, CancellationToken ct)
        {
            await applicantService.DeleteExperience(id, ct);
            return Ok();
        }
    }
}
