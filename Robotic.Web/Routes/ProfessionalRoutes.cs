using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Web.Routes;

public static class ProfessionalRoutes
{
    public static void AddProfessionalRoutes(this WebApplication app)
    {
        app.MapGet("get-professional", async (Guid id, IProfessionalRepository professionalRepository) =>
        {
            var professional = await professionalRepository.GetById(id);
            return professional == null ? Results.NoContent() : Results.Ok(professional);
        });

        app.MapGet("get-professionals", async (School? school, IProfessionalRepository professionalRepository) =>
        {
            if (!Enum.IsDefined(typeof(School), school))
            {
                return Results.BadRequest("Invalid school enum value.");
            }

            var professionals = await professionalRepository.GetAll(school);
            return professionals.Any() ? Results.Ok(professionals) : Results.NoContent();
        });

        app.MapPost("create-professional", async (Professional professional, IProfessionalRepository professionalRepository) =>
        {
            await professionalRepository.Create(professional);
            return Results.NoContent();
        });

        app.MapPut("update-professional", async (Professional professional, IProfessionalRepository professionalRepository) =>
        {
            await professionalRepository.Update(professional);
            return Results.NoContent();
        });

        app.MapDelete("delete-professional", async (Guid id, IProfessionalRepository professionalRepository) =>
        {
            await professionalRepository.Delete(id);
            return Results.NoContent();
        });
    }
}