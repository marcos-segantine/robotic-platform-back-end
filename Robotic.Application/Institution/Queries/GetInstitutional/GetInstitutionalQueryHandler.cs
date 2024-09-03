using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;

namespace Robotic.Application.Institution.Queries.GetInstitutional;

public class GetInstitutionalQueryHandler
{
    private readonly IInstitutionalRepository _repository;

    public GetInstitutionalQueryHandler(IInstitutionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<InstitutionalDTO> Handle(GetInstitutionalQuery query)
    {
        return await _repository.GetById(query.Id);
    }
}