using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Domain.Entities;
using RecruitMe.Infrastructure.Identity;

namespace RecruitMe.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Hr> Hrs { get; set; }
    public DbSet<JobPosting> JobPosts { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<InterviewSchedule> Interviews { get; set; }
    public DbSet<ApplicationEntity> Applications { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
