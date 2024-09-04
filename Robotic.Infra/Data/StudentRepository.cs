using Google.Cloud.Firestore;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;

namespace Robotic.Infra.Data;

public class StudentRepository : IStudentRepository
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("Student");
    
    public void Create(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Student Update(Student student)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<StudentDTO> GetAll(School? school)
    {
        throw new NotImplementedException();
    }
}