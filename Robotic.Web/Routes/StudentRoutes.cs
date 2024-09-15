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
        
        app.MapPost("create-student", async (Student student) =>
        {
            await studentMethods.Create(student);
            Results.NoContent();
        });
        app.MapPut("update-student", async (Student student) =>
        {
            await studentMethods.Update(student);
            Results.NoContent();
        });
        app.MapDelete("delete-student", async (Guid id) =>
        {
            await studentMethods.Delete(id);
            Results.NoContent();
        });
    }
}