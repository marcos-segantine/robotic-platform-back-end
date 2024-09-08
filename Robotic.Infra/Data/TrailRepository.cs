using Google.Cloud.Firestore;
using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;
using Robotic.Infra.Utils;

namespace Robotic.Infra.Data;

public class TrailRepository : ITrails
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("trails");

    public Task<TrailDTO> Get(Guid id)
    {
        throw new NotImplementedException();
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

    public Task AddActivities(Guid[] activities)
    {
        throw new NotImplementedException();
    }

    public Task RemoveActivities(Guid[] activities)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trail>> GetAll(Schooling? schooling)
    {
        throw new NotImplementedException();
    }
}