using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Infra.Data;

public class ActivityRepository : IActivityRepository
{
    public void Create(Activity student)
    {
        throw new NotImplementedException();
    }

    public Task<Activity> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Activity Update(Activity student)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Activity> GetAll(School? school)
    {
        throw new NotImplementedException();
    }
}