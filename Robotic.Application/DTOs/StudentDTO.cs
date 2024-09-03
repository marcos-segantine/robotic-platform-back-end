using Robotic.Domain.Enum;

public class StudentDTO
{
    public string Name { get; private set; }
    public School School { get; private set; }
    public Schooling Schooling { get; private set; }
    public string PhotoPath { get; private set; }

    public StudentDTO(string name, School school, Schooling schooling, string photoPath)
    {
        Name = name;
        School = school;
        Schooling = schooling;
        PhotoPath = photoPath;
    }
}