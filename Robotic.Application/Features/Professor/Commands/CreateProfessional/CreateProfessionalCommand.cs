namespace Robotic.Application.Features.Professor.Commands.CreateProfessional;

public class CreateProfessionalCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }
    
    public CreateProfessionalCommand(Guid id, string name, string photoPath)
    {
        Id = id;
        Name = name;
        PhotoPath = photoPath;
    }   
}