using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface ITrails
{
    Task<TrailDTO> Get(Guid id);
    Task Create(Trail trail);
    Task AddActivities(Guid[] activities);
    Task RemoveActivities(Guid[] activities);
    Task Delete(Guid id);
    Task<IEnumerable<Trail>> GetAll(Schooling? schooling);
}