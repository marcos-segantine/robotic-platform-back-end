using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Infra.Data;

public class ProfessionalRepository : IProfessionalRepository
{
    public void Create(Professional student)
    {
        throw new NotImplementedException();
    }

    public Task<ProfessionalDTO> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Professional Update(Professional student)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProfessionalDTO> GetAll(School? school)
    {
        throw new NotImplementedException();
    }
}