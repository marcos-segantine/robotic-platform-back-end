namespace Robotic.Application.Features.Professor.Commands.UpdateProfessional;

public class UpdateProfessionalCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }
    
    public UpdateProfessionalCommand(Guid id, string name, string photoPath)
    {
        Id = id;
        Name = name;
        PhotoPath = photoPath;
    }   
}