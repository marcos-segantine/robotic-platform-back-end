using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;

namespace Robotic.Application.Features.Professor.Queries.GetAllProfessional;

public class GetAllProfessionalQueryHandler
{
    private readonly IProfessionalRepository _repository;

    public GetAllProfessionalQueryHandler(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProfessionalDTO>> Handle(GetAllProfessionalQuery query)
    {
        var professionalDTOs = await _repository.GetAll(query.School);
        var professionals = professionalDTOs.Select(professional => new ProfessionalDTO(professional.Name, professional.PhotoPath));

        return professionals;
    }
}