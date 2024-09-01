namespace Robotic.Domain.Entity;

public class Activity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Resume { get; private set; }
    public string ImagePath { get; private set; }

    public Activity(Guid id, string name, string resume, string imagePath)
    {
        Id = id;
        Name = name;
        Resume = resume;
        ImagePath = imagePath;
    }
}