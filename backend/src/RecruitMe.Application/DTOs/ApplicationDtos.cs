using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.DTOs;

public class ApplicationDto
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public int JobId { get; set; }
    public JobPostingDto JobPosting { get; set; }
    public ApplicationStatus Status { get; set; }
}

public class CreateApplication
{
    public int JobId { get; set; }
}
