 using Robotic.Application.Interfaces;

 namespace Robotic.Application.Features.Students.Commands.DeleteStudent;

public class DeleteStudentCommandHandler
{
    private readonly IStudentRepository _repository;

    public DeleteStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    public void Handle(DeleteStudentCommand command)
    {
        _repository.Delete(command.Id);
    }
}