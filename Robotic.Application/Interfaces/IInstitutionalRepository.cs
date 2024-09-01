using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IInstitutionalRepository
{
    Task<Institutional> GetById(Guid id);
    Institutional Update(Institutional student);
    void Delete(Guid id);
    IEnumerable<Institutional> GetAll(School? school);
}