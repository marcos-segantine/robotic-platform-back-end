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
            snapshot.GetValue<Guid>("id"),
            snapshot.GetValue<string>("name"),
            snapshot.GetValue<string>("question"),
            snapshot.GetValue<string[]>("alternatives"),
            snapshot.GetValue<short>("points")
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
               document.GetValue<short>("points")
            );
            
            result.Add(newActivity);
        }

        return result;
    }
}