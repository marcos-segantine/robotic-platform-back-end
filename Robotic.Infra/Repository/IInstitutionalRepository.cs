using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Infra.Repository;

public class InstitutionalRepository : IInstitutionalRepository
{
    public void Create(Institutional student)
    {
        throw new NotImplementedException();
    }

    public Task<InstitutionalDTO> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Institutional Update(Institutional student)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<InstitutionalDTO> GetAll(School? school)
    {
        throw new NotImplementedException();
    }
}