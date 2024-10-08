using Robotic.Domain.Commom;

namespace Robotic.Domain.Entity;

public class Professional : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }
    
    public Professional(Guid id, string name, string photoPath)
    {
        Id = id;
        Name = name;
        PhotoPath = photoPath;
    }
}