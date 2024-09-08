using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IProfessionalRepository
{
    Task Create(Professional professional);
    Task<ProfessionalDTO> GetById(Guid id);
    Task Update(Professional student);
    Task Delete(Guid id);
    Task<IEnumerable<ProfessionalDTO>> GetAll(School? school);
}