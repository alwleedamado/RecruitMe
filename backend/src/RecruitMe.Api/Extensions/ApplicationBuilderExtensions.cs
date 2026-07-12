using Serilog;

namespace RecruitMe.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseCors("Angular");

        app.UseAuthentication();

        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwaggerUI();
        }

        app.MapHealthChecks("/health");

        app.MapControllers();

        return app;
    }
}