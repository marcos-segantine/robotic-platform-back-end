using Robotic.Domain.Entity;
using Robotic.Infra.Data;

namespace Robotic.Web.Routes;

public static class TrailRoutes
{
    public static void AddTrailRoutes(this WebApplication app)
    {
        var trailMethods = new TrailRepository();
        
        app.MapGet("get-trail", () => "Institutional Information...");
        
        app.MapPost("create-trail", async (Trail trail) =>
        {
            await trailMethods.Create(trail);
            Results.Ok();
        });
    }
}