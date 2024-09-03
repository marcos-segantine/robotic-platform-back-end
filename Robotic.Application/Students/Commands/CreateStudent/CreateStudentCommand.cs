using Robotic.Domain.Enum;

namespace Robotic.Application.Students.Commands.CreateStudent;

public class CreateStudentCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public School School { get; private set; }
    public Schooling Schooling { get; private set; }
    public string PhotoPath { get; private set; }

    public CreateStudentCommand(Guid id, string name, School school, Schooling schooling, string photoPath)
    {
        Id = id;
        Name = name;
        School = school;
        Schooling = schooling;
        PhotoPath = photoPath;
    }
}