using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IStudentRepository
{
    void Create(Student student);
    Task<Student> GetById(Guid id);
    Student Update(Student student);
    void Delete(Guid id);
    IEnumerable<StudentDTO> GetAll(School? school);
}