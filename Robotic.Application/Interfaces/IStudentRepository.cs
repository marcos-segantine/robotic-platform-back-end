using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IStudentRepository
{
    void Create(Student student);
    Task<StudentDTO> GetById(Guid id);
    Student Update(Student student);
    void Delete(Guid id);
    Task<IEnumerable<StudentDTO>> GetAll(School? school);
}