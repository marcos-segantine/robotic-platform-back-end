using Google.Cloud.Firestore;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;

namespace Robotic.Infra.Data;

public class StudentRepository : IStudentRepository
{
    private readonly CollectionReference _collectionReference;

    public StudentRepository(AppDbContext dbContext)
    {
        _collectionReference = dbContext.GetCollection("student");
    }
    
    public async Task Create(Student student)
    {
        try
        {
            var documentRef = _collectionReference.Document(student.Id.ToString());

            var studentObj = new Dictionary<string, object>
            {
                { "id", student.Id.ToString() },
                { "name", student.Name },
                { "school", student.School },
                { "schooling", student.Schooling },
                { "photoPath", student.PhotoPath },
            };
            
            await documentRef.SetAsync(studentObj);
        }
        catch (ArgumentException e)
        {
            // Consider using a logging library for better error handling
            Console.WriteLine("Argument Exception: " + e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<StudentDTO> GetById(Guid id)
    {
        try
        {
            var documentRef = _collectionReference.Document(id.ToString());
            var data = await documentRef.GetSnapshotAsync();

            if (!data.Exists)
            {
                return null;
            }

            var student = new StudentDTO(
                data.GetValue<string>("name"),
                (School)data.GetValue<int>("school"),
                (Schooling)data.GetValue<int>("schooling"),
                data.GetValue<string>("photoPath")
            );
            
            return student;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Field name not found: " + e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Update(Student student)
    {
        try
        {
            var documentRef = _collectionReference.Document(student.Id.ToString());
        
            var studentObj = new Dictionary<string, object>
            {
                { "name", student.Name },
                { "school", student.School },
                { "schooling", student.Schooling },
                { "photoPath", student.PhotoPath },
            };

            await documentRef.UpdateAsync(studentObj);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Delete(Guid id)
    {
        try
        {
            var documentRef = _collectionReference.Document(id.ToString());
            await documentRef.DeleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<StudentDTO>> GetAll(School? school)
    {
        try
        {
            Query documentsRef = school.HasValue
                ? _collectionReference.WhereEqualTo("school", (int)school)
                : _collectionReference;
        
            var data = await documentsRef.GetSnapshotAsync();
            var result = new List<StudentDTO>();

            foreach (var document in data.Documents)
            {
                var newStudent = new StudentDTO(
                    document.GetValue<string>("name"),
                    (School)document.GetValue<int>("school"),
                    (Schooling)document.GetValue<int>("schooling"),
                    document.GetValue<string>("photoPath")
                );
            
                result.Add(newStudent);
            }

            return result;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Argument Exception: " + e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
