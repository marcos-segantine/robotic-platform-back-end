namespace Robotic.Application.DTOs;

public class ActivityDTO
{
    public string Name { get; private set; }
    public string Resume { get; private set; }
    public string ImagePath { get; private set; }

    public ActivityDTO(string name, string resume, string imagePath)
    {
        Name = name;
        Resume = resume;
        ImagePath = imagePath;
    }
}