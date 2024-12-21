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
                data.GetValue<ScheduleClass>("scheduleClass"),
                data.GetValue<List<Statistics>>("statistics")
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

    public async Task<IEnumerable<StudentDTO>> GetStudentsByName(string name)
    {
        var query = _collectionReference
            .WhereGreaterThanOrEqualTo("name", name)
            .WhereLessThan("name", name + "\uf8ff");
        
        var snapshot = await query.GetSnapshotAsync();

        var students = new List<StudentDTO>();
        
        foreach (var document in snapshot.Documents)
        {
            var newStudent = new StudentDTO(
                document.GetValue<string>("name"),
                document.GetValue<School>("school"),
                document.GetValue<Schooling>("schooling"),
                document.GetValue<string>("photoPath"),
                document.GetValue<int>("points"),
                Converter.CertificationsConverter(document.GetValue<Dictionary<string, object>>("certificates")),
                document.GetValue<ScheduleClass>("scheduleClass"),
                document.GetValue<List<Statistics>>("statistics")
                );
            
            students.Add(newStudent);
        }
        
        return students;
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
                    document.GetValue<ScheduleClass>("scheduleClass"),
                    document.GetValue<List<Statistics>>("statistics")
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

    public async Task<List<Dictionary<string, object>>> GetStatistic(List<string> path)
    {
        var statistics = new TrailsStatistics();
        try
        {
            var result = await statistics.GetStatisticsFromTrail(path);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task UpdateTrailsStatistics(List<string> path, string field, object value)
    {
        TrailsStatistics statistics = new TrailsStatistics();
        
        if (value is int intValue)
        {
            await statistics.UpdateActivityState(path, field, intValue);
        }
        else if (value is bool boolValue)
        {
            await statistics.UpdateActivityState(path, field, boolValue);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public async Task CreateStatistic(List<string> path)
    {
        var statistics = new TrailsStatistics();
        try
        {
            await statistics.CreateStatistic(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public class TrailsStatistics
    {
        public async Task<List<Dictionary<string, object>>> GetStatisticsFromTrail(List<string> path)
        {
            var activityRef = new AppDbContext()
                .GetCollection("statistics")
                .Document(path[0])
                .Collection(path[1]);

            var data = await activityRef.GetSnapshotAsync();

            var trailRef = new AppDbContext()
                .GetCollection("trails")
                .Document(path[1]);
            
            var snapshot = await trailRef.GetSnapshotAsync();
            var activitiesFromTrail = snapshot.GetValue<string[]>("activities");
            
            var dataFormatted  = new List<Dictionary<string, object>>();
            
            if (data.Count == 0)
            {
                for (int i = 0; i < activitiesFromTrail.Length; i++)
                {
                    dataFormatted.Add(new Dictionary<string, object>()
                    {
                        { "isCompleted", false },
                        { "viewed", false },
                        { "points", 0 }
                    });
                }
                
                return dataFormatted;
            }
            
            foreach (var item in data)
            {
                if (activitiesFromTrail.Contains(item.Id))
                {
                    dataFormatted.Add(new Dictionary<string, object>()
                    {
                        { "isCompleted", item.GetValue<bool>("isCompleted") }, 
                        { "viewed", item.GetValue<bool>("viewed") },
                        { "points", item.GetValue<short>("points") }
                    });
                }
                else
                {
                    dataFormatted.Add(new Dictionary<string, object>()
                    {
                        { "isCompleted", false }, 
                        { "viewed", false },
                        { "points", 0 }
                    });
                }
            }

            return dataFormatted;
        }
        public async Task CreateStatistic(List<string> path)
        {
            var activityRef = new AppDbContext()
                .GetCollection("statistics")
                .Document(path[0])
                .Collection(path[1])
                .Document(path[2]);

            var data = await activityRef.GetSnapshotAsync();
            if (data.Exists)
            {
                return;
            }

            await activityRef.SetAsync(new Dictionary<string, object>
            {
                { "isCompleted", false }, 
                { "viewed", true },
                { "points", 0 }
            });
        }
        public async Task UpdateActivityState(List<string> path, string field, object value)
        {
            var activityRef = new AppDbContext()
                .GetCollection("statistics")
                .Document(path[0])
                .Collection(path[1])
                .Document(path[2]);

            await activityRef.UpdateAsync(field, value);
        }
    }
}