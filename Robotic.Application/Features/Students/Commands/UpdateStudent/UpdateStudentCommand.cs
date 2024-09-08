using Robotic.Domain.Enum;

namespace Robotic.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public School School { get; private set; }
    public Schooling Schooling { get; private set; }
    public string PhotoPath { get; private set; }

    public UpdateStudentCommand(Guid id, string name, School school, Schooling schooling, string photoPath)
    {
        Id = id;
        Name = name;
        School = school;
        Schooling = schooling;
        PhotoPath = photoPath;
    }
}