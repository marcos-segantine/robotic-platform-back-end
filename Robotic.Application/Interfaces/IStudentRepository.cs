using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IStudentRepository
{
    Task Create(Student student);
    Task<StudentDTO> GetById(Guid id);
    Task Update(Student student);
    Task Delete(Guid id);
    Task<IEnumerable<StudentDTO>> GetAll(School? school);
}