using Google.Cloud.Firestore;
using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;
using Robotic.Infra.Utils;

namespace Robotic.Infra.Repository;

public class ActivityRepository : IActivityRepository
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("activities");
    
    public async Task Create(Activity activity)
    {
        try
        {
            var documentRef = _collectionReference.Document(activity.Id.ToString());
            var activityObj = DataUtils.FormatDataToDb(activity);
            
            await documentRef.SetAsync(activityObj);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ActivityDTO> GetById(Guid id)
    {
        var documentRef = _collectionReference.Document(id.ToString());
            
        var snapshot = await documentRef.GetSnapshotAsync();

        var data = new ActivityDTO(
            Guid.Parse(snapshot.GetValue<string>("id")),
            snapshot.GetValue<string>("title"),
            snapshot.GetValue<string>("question"),
            snapshot.GetValue<string[]>("alternatives"),
            snapshot.GetValue<short>("weight"),
            snapshot.GetValue<string[]>("resources"),
            snapshot.GetValue<string>("summarize"),
            snapshot.GetValue<string>("explanation"),
            snapshot.GetValue<string>("rightResponse")
            );
        
        return data;
    }

    public async Task Update(ActivityDTO activity)
    {
        try
        {
            var documentRef = _collectionReference.Document(activity.Id.ToString());
            
            var activityObj = DataUtils.FormatDataToDb(activity);
            activityObj.Add("modifiedOn", DataUtils.UpdateTime());

            await documentRef.UpdateAsync(activityObj);
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

    public async Task<IEnumerable<ActivityDTO>> GetAll(School? school)
    {
        Query documentsRef = school.HasValue ? _collectionReference.WhereEqualTo("school", school) : _collectionReference;

        var data = await documentsRef.GetSnapshotAsync();
        var result = new List<ActivityDTO>();

        foreach (var document in data.Documents)
        {
            var newActivity = new ActivityDTO (
               document.GetValue<Guid>("id"),
               document.GetValue<string>("name"),
               document.GetValue<string>("question"),
               document.GetValue<string[]>("alternatives"),
               document.GetValue<short>("weight"),
               document.GetValue<string[]>("resources"),
               document.GetValue<string>("summarize"),
               document.GetValue<string>("explanation"),
               document.GetValue<string>("rightResponse")
            );
            
            result.Add(newActivity);
        }

        return result;
    }

    public async Task<List<Dictionary<string, object>>> GetRanking(List<string>? path)
    {
        try
        {
            var students = await new AppDbContext().GetCollection("student").GetSnapshotAsync();

            var result = new List<Dictionary<string, object>>();
            
            foreach (var student in students)
            {
                result.Add(new Dictionary<string, object>()
                {
                    { "name", student.GetValue<string>("name") },
                    { "points", student.GetValue<int>("points") },
                    { "photoPath", student.GetValue<string>("photoPath") },
                    { "school", student.GetValue<School>("school") }
                });
            }
            
            var resultSorted = result.OrderByDescending(d => (int)d["points"]).ToList();
            return resultSorted;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}