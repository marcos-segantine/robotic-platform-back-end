using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Data;

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
        
        app.MapGet("get-activities", async (School? school) =>
        {
            if (school != null && Enum.IsDefined(typeof(School), school) == false)
            {
                Results.BadRequest();
            }
            
            var activities = await activityMethods.GetAll(school);
            
            return activities.Any() ? Results.Ok(activities) : Results.NoContent();
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