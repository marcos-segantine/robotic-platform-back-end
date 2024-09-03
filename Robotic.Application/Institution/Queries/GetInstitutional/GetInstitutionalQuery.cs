namespace Robotic.Application.Institution.Queries.GetInstitutional;

public class GetInstitutionalQuery
{
    public Guid Id { get; private set; }

    public GetInstitutionalQuery(Guid id)
    {
        Id = id;
    }
}