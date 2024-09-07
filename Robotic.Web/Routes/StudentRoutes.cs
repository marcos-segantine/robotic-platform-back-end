using Robotic.Infra.Data;

namespace Robotic.Web.Routes;

public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        var studentMethods = new StudentRepository();
        
        app.MapGet("get-student", (Guid id) => studentMethods.GetById(id));
    }
}