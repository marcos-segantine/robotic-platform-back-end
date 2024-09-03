using Robotic.Domain.Enum;

namespace Robotic.Application.Institution.Queries.GetAllInstitutional;

public class GetAllInstitutionalQuery
{
    public School? School { get; private set; }

    public GetAllInstitutionalQuery(School school)
    {
        School = school;
    }
}