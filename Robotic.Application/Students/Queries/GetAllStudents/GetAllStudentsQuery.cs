using Robotic.Domain.Enum;

public class GetAllStudentsQuery
{
    public School? School { get; private set; }

    public GetAllStudentsQuery(School? school)
    {
        School = school;
    }
}