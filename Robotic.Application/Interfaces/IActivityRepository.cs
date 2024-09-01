using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IActivityRepository
{
    Task<Activity> GetById(Guid id);
    Activity Update(Activity student);
    void Delete(Guid id);
    IEnumerable<Activity> GetAll(School? school);
}