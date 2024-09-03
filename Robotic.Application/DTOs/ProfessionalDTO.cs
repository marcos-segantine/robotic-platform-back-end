namespace Robotic.Application.DTOs;

public class ProfessionalDTO
{
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }

    public ProfessionalDTO(string name, string photoPath)
    {
        Name = name;
        PhotoPath = photoPath;
    }
}