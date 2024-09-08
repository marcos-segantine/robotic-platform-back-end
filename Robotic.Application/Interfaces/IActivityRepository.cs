using Robotic.Application.DTOs;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;

namespace Robotic.Application.Interfaces;

public interface IActivityRepository
{
    Task Create(Activity activity);
    Task<ActivityDTO> GetById(Guid id);
    Task Update(Activity student);
    Task Delete(Guid id);
    Task<IEnumerable<ActivityDTO>> GetAll(School? school);
}