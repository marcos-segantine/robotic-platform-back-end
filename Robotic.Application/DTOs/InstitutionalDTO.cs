namespace Robotic.Application.DTOs;

public class InstitutionalDTO
{
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }

    public InstitutionalDTO(string name, string photoPath)
    {
        Name = name;
        PhotoPath = photoPath;
    }
}