namespace Robotic.Web.Routes;

public static class ActivityRoutes
{
    public static void AddActivityRoutes(this WebApplication app)
    {
        app.MapGet("get-activity", () => "Activity Information...");
    }
}