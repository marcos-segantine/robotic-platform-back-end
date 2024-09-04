namespace Robotic.Web.Routes;

public static class ProfessionalRoutes
{
    public static void AddProfessionalRoutes(this WebApplication app)
    {
        app.MapGet("get-professional", () => "Professional Information...");
    }
}