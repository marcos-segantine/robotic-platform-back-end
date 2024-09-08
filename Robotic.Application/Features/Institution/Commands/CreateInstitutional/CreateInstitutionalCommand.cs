namespace Robotic.Application.Features.Institution.Commands.CreateInstitutional;

public class CreateInstitutionalCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }

    public CreateInstitutionalCommand(Guid id, string name, string photoPath)
    {
        Id = id;
        Name = name;
        PhotoPath = photoPath;
    }
}