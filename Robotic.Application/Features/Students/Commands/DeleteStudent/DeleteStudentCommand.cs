namespace Robotic.Application.Features.Students.Commands.DeleteStudent;

public class DeleteStudentCommand
{
    public Guid Id { get; private set; }

    public DeleteStudentCommand(Guid id)
    {
        Id = id;
    }
}