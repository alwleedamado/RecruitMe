namespace RecruitMe.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddOpenApi(configuration);

        services.AddJwtAuthentication(configuration);

        services.AddIdentityServices(configuration);

        services.AddAuthorizationPolicies();

        services.AddCorsPolicy(configuration);

        services.AddHealthChecks(configuration);

        return services;
    }
}