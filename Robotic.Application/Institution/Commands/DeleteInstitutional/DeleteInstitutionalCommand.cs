namespace Robotic.Application.Institution.Commands.DeleteInstitutional;

public class DeleteInstitutionalCommand
{
    public Guid Id { get; private set; }

    public DeleteInstitutionalCommand(Guid id)
    {
        Id = id;
    }
}