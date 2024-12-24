using System.Reflection.Metadata;
using Google.Cloud.Firestore;
using Robotic.Application.DTOs;
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
    
    public async Task<Dictionary<string, object>> GetLeaningProcess(Guid studentID)
    {
        try
        {
            TrailsStatistics statistics = new TrailsStatistics();
        
            var result = await statistics.GetLeaningProcess(studentID);
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
    
    public async Task MarkActivityAsDone(List<string> path, short points)
    {
        try
        {
            TrailsStatistics statistics = new TrailsStatistics();

            await statistics.UpdateActivityState(path, "points", points);
            await statistics.UpdateActivityState(path, "isCompleted", true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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

    public async Task<List<ActivityDTO>> GetActivitiesNotFinished(Guid userID, List<string> trailsID)
    {
        var studentStatisticRef = new AppDbContext()
            .GetCollection("statistics")
            .Document(userID.ToString());

        var data = new List<ActivityDTO>();
        var activity = new ActivityRepository();
        
        foreach (var trailID in trailsID)
        {
            if (data.Count == 3)
            {
                break;
            }
            
            var trailRef = studentStatisticRef.Collection(trailID);
            var snapshot = await trailRef.GetSnapshotAsync();

            foreach (var doc in snapshot)
            {
                if (doc.GetValue<bool>("isCompleted") == false)
                {
                    data.Add(await activity.GetById(Guid.Parse(doc.Id)));
                }
            }
        }
        
        return data;
    }
    
    public class TrailsStatistics
    {
        public async Task<List<Dictionary<string, object>>> GetStatisticsFromTrail(List<string> path)
        {
            if (path.Count == 3)
            {
                var activityStatisticRef = new AppDbContext()
                    .GetCollection("statistics")
                    .Document(path[0])
                    .Collection(path[1])
                    .Document(path[2]);
                
                var activityStatisticData = await activityStatisticRef.GetSnapshotAsync();

                var result = new List<Dictionary<string, object>>();
                
                    
                if (!activityStatisticData.Exists)
                {
                    result.Add(new Dictionary<string, object>()
                    {
                        { "isCompleted", false }, 
                        { "viewed", false },
                        { "points", 0 }
                    });
                    return result;
                }
                
                result.Add(new Dictionary<string, object>()
                {
                    { "isCompleted", activityStatisticData.GetValue<bool>("isCompleted") }, 
                    { "viewed", activityStatisticData.GetValue<bool>("viewed") },
                    { "points", activityStatisticData.GetValue<short>("points") }
                });
                
                return result;
            }
            
            var trailStatisticRef = new AppDbContext()
                .GetCollection("statistics")
                .Document(path[0])
                .Collection(path[1]);

            var data = await trailStatisticRef.GetSnapshotAsync();
            var activitiesInStatistics = data.Documents.Select(item => item.Id);
            
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
            
            foreach (var item in activitiesFromTrail)
            {
                if (activitiesInStatistics.Contains(item))
                {
                    var activityStatistic = await trailStatisticRef.Document(item).GetSnapshotAsync();
                    dataFormatted.Add(new Dictionary<string, object>()
                    {
                        { "isCompleted", activityStatistic.GetValue<bool>("isCompleted") }, 
                        { "viewed", activityStatistic.GetValue<bool>("viewed") },
                        { "points", activityStatistic.GetValue<short>("points") }
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
        public async Task<Dictionary<string, object>> GetLeaningProcess(Guid studentID)
        {
            var trailRef = new AppDbContext().GetCollection("trails");
            var trails = await trailRef.GetSnapshotAsync();

            var result = new Dictionary<string, object>
            {
                { "activitiesCompleted", 0 },
                { "activitiesViewed", 0 },
                { "activitiesCount", 0 },
                { "trailsCompleted", 0 },
                { "trailsCount", 0 },
            };
            
            foreach (var trail in trails)
            {
                var activities = trail.GetValue<string[]>("activities");
                var activitiesCompleted = 0;

                result["trailsCount"] = (int)result["trailsCount"] + 1;
                
                foreach (var activityID in activities)
                {
                    var statistic = new AppDbContext()
                        .GetCollection("statistics")
                        .Document(studentID.ToString())
                        .Collection(trail.Id)
                        .Document(activityID);
                    
                    var statisticData = await statistic.GetSnapshotAsync();

                    if (!statisticData.Exists)
                    {
                        result["activitiesCount"] = (int)result["activitiesCount"] + activities.Length;
                        continue;
                    }
                    
                    if (statisticData.GetValue<bool>("isCompleted"))
                    {
                        result["activitiesCompleted"] = (int)result["activitiesCompleted"] + 1;
                        activitiesCompleted++;
                    }
                    if (statisticData.GetValue<bool>("viewed"))
                    {
                        result["activitiesViewed"] = (int)result["activitiesViewed"] + 1;
                    }
                    
                    result["activitiesCount"] = (int)result["activitiesCount"] + 1;
                }

                if (activitiesCompleted == activities.Length)
                {
                    result["trailsCompleted"] = (int)result["trailsCompleted"] + 1;
                }
            }

            return result;
        }
    }
}