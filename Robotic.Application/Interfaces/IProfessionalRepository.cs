using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IProfessionalRepository
{
    Task<Professional> GetById(Guid id);
    Professional Update(Professional student);
    void Delete(Guid id);
    IEnumerable<Professional> GetAll(School? school);
}