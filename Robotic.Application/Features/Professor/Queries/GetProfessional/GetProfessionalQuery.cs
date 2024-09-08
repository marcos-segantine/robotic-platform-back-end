namespace Robotic.Application.Features.Professor.Queries.GetProfessional;

public class GetProfessionalQuery
{
    public Guid Id { get; private set; }

    public GetProfessionalQuery(Guid id)
    {
        Id = id;
    }
}