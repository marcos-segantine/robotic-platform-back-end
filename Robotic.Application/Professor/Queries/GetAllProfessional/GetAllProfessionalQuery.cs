using Robotic.Domain.Enum;

namespace Robotic.Application.Professor.Queries.GetAllProfessional;

public class GetAllProfessionalQuery
{
    public School? School { get; private set; }

    public GetAllProfessionalQuery(School? school)
    {
        School = school;
    }
}