namespace Robotic.Web.Routes;

public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        app.MapGet("get-student", () => "Student Information...");
    }
}