using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Data;

namespace Robotic.Web.Routes;

public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        var studentMethods = new StudentRepository();
        
        app.MapGet("get-student", (Guid id) => studentMethods.GetById(id));
        app.MapGet("get-students", (School? school) => studentMethods.GetAll(school));
        
        app.MapPost("create-student", (Student student) => studentMethods.Create(student));
        app.MapPut("update-student", (Student student) => studentMethods.Update(student));
        app.MapDelete("delete-student", (Guid id) => studentMethods.Delete(id));
    }
}