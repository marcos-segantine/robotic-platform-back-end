using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Web.Routes;

public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        app.MapGet("get-student", async (Guid id, IStudentRepository studentRepository) =>
        {
            var student = await studentRepository.GetById(id);
            return student == null ? Results.NoContent() : Results.Ok(student);
        });

        app.MapGet("get-students", async (School? school, IStudentRepository studentRepository) =>
        {
            if (!Enum.IsDefined(typeof(School), school))
            {
                return Results.BadRequest("Invalid school enum value.");
            }

            var students = await studentRepository.GetAll(school);
            return students.Any() ? Results.Ok(students) : Results.NoContent();
        });

        app.MapPost("create-student", async (Student student, IStudentRepository studentRepository) =>
        {
            await studentRepository.Create(student);
            return Results.NoContent();
        });

        app.MapPut("update-student", async (Student student, IStudentRepository studentRepository) =>
        {
            await studentRepository.Update(student);
            return Results.NoContent();
        });

        app.MapDelete("delete-student", async (Guid id, IStudentRepository studentRepository) =>
        {
            await studentRepository.Delete(id);
            return Results.NoContent();
        });
    }
}