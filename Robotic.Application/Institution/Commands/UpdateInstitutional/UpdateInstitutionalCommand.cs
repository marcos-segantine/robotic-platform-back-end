namespace Robotic.Application.Institution.Commands.UpdateInstitutional;

public class UpdateInstitutionalCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string PhotoPath { get; private set; }

    public UpdateInstitutionalCommand(Guid id, string name, string photoPath)
    {
        Id = id;
        Name = name;
        PhotoPath = photoPath;
    }
}