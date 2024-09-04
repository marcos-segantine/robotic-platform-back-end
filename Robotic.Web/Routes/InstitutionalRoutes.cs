namespace Robotic.Web.Routes;

public static class InstitutionalRoutes
{
    public static void AddInstitutionalRoutes(this WebApplication app)
    {
        app.MapGet("get-institutional", () => "Institutional Information...");
    }
}