using Google.Cloud.Firestore;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;
using Robotic.Infra.Utils;

namespace Robotic.Infra.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("student");
    
    public async Task Create(Student student)
    {
        try
        {
            var documentRef = _collectionReference.Document(student.Id.ToString());
            var studentObj = DataUtils.FormatDataToDb(student);
            
            await documentRef.SetAsync(studentObj);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Argument Exception");
            Console.WriteLine(e.Message);
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

            if (data.Exists == false)
            {
                return null;
            }

            var student = new StudentDTO (
                data.GetValue<string>("name"),
                (School)data.GetValue<int>("school"),
                (Schooling)data.GetValue<int>("schooling"),
                data.GetValue<string>("photoPath"),
                data.GetValue<int>("points"),
                Converter.CertificationsConverter(data.GetValue<Dictionary<string, object>>("certificates")),
                data.GetValue<ScheduleClass>("scheduleClass")
                );
            
            return student;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Field name not found!");
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
            
            var studentObj = DataUtils.FormatDataToDb(student);

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
            Query documentsRef;
        
            if (school.HasValue)
            {
                documentsRef = _collectionReference.WhereEqualTo("school", school.ToString());
            }
            else
            {
                documentsRef = _collectionReference;
            }
        
            var data = await documentsRef.GetSnapshotAsync();
            var result = new List<StudentDTO>();

            foreach (var document in data.Documents)
            {
                var newStudent = new StudentDTO (
                    document.GetValue<string>("name"),
                    (School)document.GetValue<int>("school"),
                    (Schooling)document.GetValue<int>("schooling"),
                    document.GetValue<string>("photoPath"),
                    document.GetValue<int>("points"),
                    document.GetValue<Certifications>("certifications"),
                    document.GetValue<ScheduleClass>("scheduleClass")
                );
            
                result.Add(newStudent);
            }

            return result;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Argument Exception");
            Console.WriteLine(e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}