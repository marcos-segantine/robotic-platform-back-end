 using Robotic.Application.Interfaces;
 using Robotic.Domain.Entity;

 namespace Robotic.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommandHandler
{
    private readonly IStudentRepository _repository;

    public UpdateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    public void Handle(UpdateStudentCommand command)
    {
        var student = new Student(command.Id, command.Name, command.School, command.Schooling, command.PhotoPath);

        _repository.Update(student);
    }
}