using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;

namespace Robotic.Application.Professor.Queries.GetProfessional;

public class GetProfessionalQueryHandle
{
    private readonly IProfessionalRepository _repository;

    public GetProfessionalQueryHandle(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProfessionalDTO> Handle(GetProfessionalQuery query)
    {
        return await _repository.GetById(query.Id);
    }
}