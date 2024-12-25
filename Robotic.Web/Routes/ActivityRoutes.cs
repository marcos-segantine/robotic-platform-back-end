using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Repository;

namespace Robotic.Web.Routes;

public static class ActivityRoutes
{
    public static void AddActivityRoutes(this WebApplication app)
    {
        var activityMethods = new ActivityRepository();
        
        app.MapGet("get-activity", async (Guid id) =>
        {
            var activity = await activityMethods.GetById(id);
            return activity == null ? Results.NoContent() : Results.Ok(activity);
        });
        
        app.MapGet("get-ranking", async (string? path) =>
        {
            var pathSplit = path?.Split(",").ToList() ?? null;
            var result = await activityMethods.GetRanking(pathSplit);
            return Results.Ok(result);
        });
        
        app.MapPost("create-activity", async (Activity activity) =>
        {
            await activityMethods.Create(activity);
            Results.NoContent();
        });
        
        app.MapPut("update-activity", async (ActivityDTO activity) =>
        {
            await activityMethods.Update(activity);
            Results.NoContent();
        });
        
        app.MapDelete("delete-activity", async (Guid id) =>
        {
            await activityMethods.Delete(id);
            Results.NoContent();
        });
    }
}