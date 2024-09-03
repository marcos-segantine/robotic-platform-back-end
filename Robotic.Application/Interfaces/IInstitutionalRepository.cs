using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IInstitutionalRepository
{
    void Create(Institutional institutional);
    Task<InstitutionalDTO> GetById(Guid id);
    Institutional Update(Institutional student);
    void Delete(Guid id);
    IEnumerable<InstitutionalDTO> GetAll(School? school);
}