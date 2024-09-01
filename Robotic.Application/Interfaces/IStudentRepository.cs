using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IStudentRepository
{
    Task<Student> GetById(Guid id);
    Student Update(Student student);
    void Delete(Guid id);
    IEnumerable<Student> GetAll(School? school);
}