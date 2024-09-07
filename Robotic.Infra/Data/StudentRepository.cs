using Google.Cloud.Firestore;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;

namespace Robotic.Infra.Data;

public class StudentRepository : IStudentRepository
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("student");
    
    public void Create(Student student)
    {
        throw new NotImplementedException();
    }

    public async Task<Student> GetById(Guid id)
    {
        try
        {
            var documentRef = _collectionReference.Document(id.ToString());
            var data = await documentRef.GetSnapshotAsync();

            if (data.Exists)
            {
                var student = new Student (id, "John Doe", School.Aparecida, Schooling.ElementarySchool, "John Doe");
                return student;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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