using System.Reflection.Metadata;
using Google.Cloud.Firestore;
using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;
using Robotic.Infra.Utils;

namespace Robotic.Infra.Repository;

public class TrailRepository : ITrails
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("trails");
    private readonly CollectionReference _activitiesCollectionReference = new AppDbContext().GetCollection("activities");

    public async Task<TrailDTO> Get(Guid id)
    {
        try
        {
            var documentRef = _collectionReference.Document(id.ToString());
            var snapshot = await documentRef.GetSnapshotAsync();

            var data = new TrailDTO(
                snapshot.GetValue<Guid>("id"),
                snapshot.GetValue<string>("name"),
                snapshot.GetValue<string>("summarize"),
                snapshot.GetValue<Difficulty>("difficulty"),
                snapshot.GetValue<Guid[]>("activities"),
                snapshot.GetValue<Schooling>("schooling")
            );

            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Create(Trail trail)
    {
        try
        {
            var documentRef = _collectionReference.Document(trail.Id.ToString());
            var trailObj = DataUtils.FormatDataToDb(trail);
            
            await documentRef.SetAsync(trailObj);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddActivities(Guid id, Guid[] activities)
    {
        var documentRef = _collectionReference.Document(id.ToString());
        var snapshot = await documentRef.GetSnapshotAsync();

        var activitiesTrail = new List<Guid>(GuidUtils.StringToGuidArray(snapshot.GetValue<string[]>("activities")));
        var activitiesTrailCopy = new List<Guid>(activities);
        
        foreach (var document in activitiesTrail)
        {
            if (activities.Contains(document))
            {
                activitiesTrailCopy.Add(document);
            }
        }

        await documentRef.UpdateAsync("activities", FieldValue.ArrayUnion(GuidUtils.GuidToStringArray(activitiesTrailCopy.ToArray())));
    }

    public async Task<IEnumerable<ActivityDTO>> GetActivities(Guid id)
    {
        var documentRef = _collectionReference.Document(id.ToString());
        var snapshot = await documentRef.GetSnapshotAsync();
        var activitiesIDs = snapshot.GetValue<List<string>>("activities");
            
        var result = new List<ActivityDTO>();

        foreach (var activityID in activitiesIDs)
        {
            var activityRef = _activitiesCollectionReference.Document(activityID);
            var data = await activityRef.GetSnapshotAsync();

            var dataFormatted = new ActivityDTO(
                Guid.Parse(data.GetValue<string>("id")),
                data.GetValue<string>("title"),
                data.GetValue<string>("question"),
                data.GetValue<string[]>("alternatives"),
                data.GetValue<short>("points"),
                data.GetValue<string[]>("resources"),
                data.GetValue<string>("summarize"),
                data.GetValue<string>("explanation")
                );
            
            result.Add(dataFormatted);
        }

        return result;
    }

    public async Task RemoveActivities(Guid id, Guid[] activities)
    {
        var documentRef = _collectionReference.Document(id.ToString());
        var snapshot = await documentRef.GetSnapshotAsync();

        var activitiesTrail = new List<Guid>(GuidUtils.StringToGuidArray(snapshot.GetValue<string[]>("activities")));
        var activitiesTrailCopy = new List<Guid>(activities);
        
        foreach (var document in activitiesTrail)
        {
            if (activities.Contains(document))
            {
                activitiesTrailCopy.Remove(document);
            }
        }
        
        await documentRef.UpdateAsync("activities", activitiesTrailCopy);
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

    public async Task<IEnumerable<TrailDTO>> GetAll(Schooling? schooling)
    {
        try
        {
            Query documentRef = schooling != null ? 
                _collectionReference.WhereEqualTo("schooling", schooling) :
                _collectionReference;
            
            var snapshot = await documentRef.GetSnapshotAsync();

            var result = new List<TrailDTO>();
            
            foreach (var document in snapshot.Documents)
            {
                var id = Guid.Parse(document.GetValue<string>("id"));
                var activities = document.GetValue<List<string>>("activities")
                    .Select(item => Guid.Parse(item.ToString())).ToArray();
                    
                var data = new TrailDTO(
                    id,
                    document.GetValue<string>("name"),
                    document.GetValue<string>("summarize"),
                    document.GetValue<Difficulty>("difficulty"),
                    activities,
                    document.GetValue<Schooling>("schooling")
                );
                
                result.Add(data);
            }

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}