using AutoMapper;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.DTOs;

public class MappingProfile : Profile
{
   public MappingProfile()
    {
        CreateMap<Hr, HrDto>();
    }
}