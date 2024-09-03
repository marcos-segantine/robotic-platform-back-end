namespace Robotic.Application.Students.Queries.GetStudent;

public class GetStudentQuery
{
    public Guid Id { get; private set; }

    public GetStudentQuery(Guid id)
    {
        Id = id;
    }
}