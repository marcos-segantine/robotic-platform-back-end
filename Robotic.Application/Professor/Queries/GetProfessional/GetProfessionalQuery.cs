namespace Robotic.Application.Professor.Queries.GetProfessional;

public class GetProfessionalQuery
{
    public Guid Id { get; private set; }

    public GetProfessionalQuery(Guid id)
    {
        Id = id;
    }
}