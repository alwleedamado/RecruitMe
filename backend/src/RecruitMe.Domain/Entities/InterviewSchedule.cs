using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Domain.Entities;

public class InterviewSchedule : AuditEntity
{
    public DateTime ShceduledDate { get; set; }
    public InterviewStatus Status { get; set; }
}

public enum InterviewStatus
{
    Scheduled,
    Attended,
    Canceled,
    Missed
}
