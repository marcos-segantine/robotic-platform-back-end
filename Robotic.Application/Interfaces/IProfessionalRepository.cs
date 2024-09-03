using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IProfessionalRepository
{
    void Create(Professional professional);
    Task<ProfessionalDTO> GetById(Guid id);
    Professional Update(Professional student);
    void Delete(Guid id);
    IEnumerable<ProfessionalDTO> GetAll(School? school);
}