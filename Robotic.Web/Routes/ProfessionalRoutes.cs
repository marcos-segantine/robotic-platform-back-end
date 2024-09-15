using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Repository;

namespace Robotic.Web.Routes;

public static class ProfessionalRoutes
{
    public static void AddProfessionalRoutes(this WebApplication app)
    {
        var professionalMethods = new ProfessionalRepository();
        
        app.MapGet("get-professional", async (Guid id) =>
        {
            var professional = await professionalMethods.GetById(id);
            return professional == null ? Results.NoContent() : Results.Ok(professional);
        });
        
        app.MapGet("get-professionals", async (School? school) =>
        {
            if (Enum.IsDefined(typeof(School), school) == false)
            {
                Results.BadRequest();
            }
            
            var professionals = await professionalMethods.GetAll(school);
            return professionals.Any() ? Results.Ok(professionals) : Results.NoContent();
        });
        
        app.MapPost("create-professional", async (Professional professional) =>
        {
            await professionalMethods.Create(professional);
            Results.NoContent();
        });
        app.MapPut("update-professional", async (Professional professional) =>
        {
            await professionalMethods.Update(professional);
            Results.NoContent();
        });
        app.MapDelete("delete-professional", async (Guid id) =>
        {
            await professionalMethods.Delete(id);
            Results.NoContent();
        });
    }
}