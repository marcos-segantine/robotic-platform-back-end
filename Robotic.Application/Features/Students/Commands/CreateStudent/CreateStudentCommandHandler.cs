 using Robotic.Application.Interfaces;
 using Robotic.Domain.Entity;

 namespace Robotic.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommandHandler
{
    private readonly IStudentRepository _repository;

    public CreateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    public void Handle(CreateStudentCommand command)
    {
        var student = new Student(command.Id, command.Name, command.School, command.Schooling, command.PhotoPath, command.Points, command.Certificates);

        _repository.Create(student);
    }
}