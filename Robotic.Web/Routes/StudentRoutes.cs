using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Repository;

namespace Robotic.Web.Routes;

public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        var studentMethods = new StudentRepository();
        
        app.MapGet("get-student", async (Guid id) =>
        {
            var student = await studentMethods.GetById(id);
            return student == null ? Results.NoContent() : Results.Ok(student);
        });
        
        app.MapGet("get-students", async (School? school) =>
        {
            if (school != null && Enum.IsDefined(typeof(School), school) == false)
            {
                Results.BadRequest();
            }
            
            var students = await studentMethods.GetAll(school);
            
            return students.Any() ? Results.Ok(students) : Results.NoContent();
        });

        app.MapGet("get-students-by-name", async (string name) =>
        {
            var response = await studentMethods.GetStudentsByName(name);

            if (response.Count() == 0)
            {
                return Results.NoContent();
            }
            else
            {
                return Results.Ok(response);
            }
        });
        
        app.MapGet("get-activities-not-viewed", async (Guid userID, string trailsID) =>
        {
            var trailsIDArray = trailsID.Split(',').ToList();
            var response = await studentMethods.GetActivitiesNotViewed(userID, trailsIDArray);
            return response != null && response.Count() > 0 ? Results.Ok(response) : Results.NoContent();
        });
        
        app.MapPost("create-student", async (Student student) =>
        {
            await studentMethods.Create(student);
            Results.NoContent();
        });
        
        app.MapGet("get-statistic", async (string path) =>
        {
            var pathSplit = path.Split(",").ToList();
            
            var result = await studentMethods.GetStatistic(pathSplit);
            return result != null ? Results.Ok(result) : Results.NoContent();
        });
        
        app.MapPost("create-statistic", async (List<string> path) =>
        {
            await studentMethods.CreateStatistic(path);
            Results.NoContent();
        });
        
        app.MapPut("update-student", async (Student student) =>
        {
            await studentMethods.Update(student);
            Results.NoContent();
        });
        
        app.MapPut("update-statistics", async (List<string> path, string field, int value) =>
        {
            if (field == "points")
            {
                await studentMethods.UpdateTrailsStatistics(path, field, value);
                return Results.NoContent();
            }
            else
            {
                var boolValue = value == 0 ? false : true;
                studentMethods.UpdateTrailsStatistics(path, field, boolValue);
                return Results.NoContent();
            }
        });
        
        app.MapDelete("delete-student", async (Guid id) =>
        {
            await studentMethods.Delete(id);
            Results.NoContent();
        });
    }
}