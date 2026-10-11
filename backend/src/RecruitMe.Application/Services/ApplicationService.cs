using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task CreateApplication(CreateApplication request)
        {

        }
    }
}
