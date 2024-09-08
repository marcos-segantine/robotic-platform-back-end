using Google.Cloud.Firestore;
using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;
using Robotic.Infra.Utils;

namespace Robotic.Infra.Data;

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
            snapshot.GetValue<string>("name"),
            snapshot.GetValue<string>("resume"),
            snapshot.GetValue<string>("imagePath")
            );
        
        return data;
    }

    public async Task Update(Activity activity)
    {
        try
        {
            var documentRef = _collectionReference.Document(activity.Id.ToString());
            var activityObj = DataUtils.FormatDataToDb(activity);
            
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
                document.GetValue<string>("name"),
                document.GetValue<string>("resume"),
                document.GetValue<string>("photoPath")
            );
            
            result.Add(newActivity);
        }

        return result;
    }
}