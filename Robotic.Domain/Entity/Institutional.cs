using Robotic.Domain.Commom;

namespace Robotic.Domain.Entity;

public class Institutional : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string ImagePath { get; private set; }

    public Institutional(Guid id, string name, string imagePath)
    {
        Id = id;
        Name = name;
        ImagePath = imagePath;
    }
}