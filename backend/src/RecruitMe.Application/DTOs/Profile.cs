using AutoMapper;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.DTOs;

public class MappingProfile : Profile
{
   public MappingProfile()
    {
        CreateMap<Hr, HrDto>();
        CreateMap<UpdateHr, Hr>();
        CreateMap<JobPosting, JobPostingDto>();
        CreateMap<Applicant, ApplicantDto>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.SkillName));
        CreateMap<WorkExperience, WorkExperienceDto>();
    }
}
+
