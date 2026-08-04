using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.DTOs;

public class CreateApplicant
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class ApplicantDto
{
    public int ID { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public List<WorkExperienceDto> WorkExperiences { get; set; } = [];
    public List<string> Skills { get; set; } = [];
}

public class WorkExperienceDto
{
    public required string CompanyName { get; set; }
    public required string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ApplicaId { get; set; }
}
