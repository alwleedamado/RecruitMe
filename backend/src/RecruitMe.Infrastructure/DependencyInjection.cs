using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RecruitMe.Application.Authentication.Interfaces;
using RecruitMe.Infrastructure.Authentication;
using RecruitMe.Infrastructure.Email;
using RecruitMe.Infrastructure.Identity;
using RecruitMe.Infrastructure.Options;
using RecruitMe.Infrastructure.Presistence;
using System.Text;

namespace RecruitMe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ------------------------------------------------------------
        // Database
        // ------------------------------------------------------------

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });

        // ------------------------------------------------------------
        // Options
        // ------------------------------------------------------------

        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.Configure<EmailOptions>(
            configuration.GetSection(EmailOptions.SectionName));

        // ------------------------------------------------------------
        // Identity
        // ------------------------------------------------------------

        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // ------------------------------------------------------------
        // JWT Authentication
        // ------------------------------------------------------------

        var jwtOptions = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()
            ?? throw new InvalidOperationException("JWT configuration is missing.");

        var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ClockSkew = TimeSpan.Zero
                };
            });

        // ------------------------------------------------------------
        // Authorization
        // ------------------------------------------------------------

        services.AddAuthorization();

        // ------------------------------------------------------------
        // AutoMapper
        // ------------------------------------------------------------

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // ------------------------------------------------------------
        // FluentValidation
        // ------------------------------------------------------------

        services.AddValidatorsFromAssemblies(
            AppDomain.CurrentDomain.GetAssemblies());

        // ------------------------------------------------------------
        // Application Services
        // ------------------------------------------------------------

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}