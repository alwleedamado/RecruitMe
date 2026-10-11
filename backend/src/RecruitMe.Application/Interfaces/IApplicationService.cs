using RecruitMe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Interfaces
{
    public  interface IApplicationService
    {
        Task CreateApplication(CreateApplication request);
    }
}
