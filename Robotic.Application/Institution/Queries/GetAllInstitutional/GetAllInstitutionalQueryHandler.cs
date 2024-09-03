using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Institution.Queries.GetAllInstitutional;

public class GetAllInstitutionalQueryHandler
{
    private readonly IInstitutionalRepository _repository;

    public GetAllInstitutionalQueryHandler(IInstitutionalRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<InstitutionalDTO> Handle(GetAllInstitutionalQuery query)
    {
        var institutional = _repository.GetAll(query.School);
        var institutionalDTOs = institutional.Select(institutional => new InstitutionalDTO(institutional.Name, institutional.PhotoPath)).ToList();
    
        return institutionalDTOs;
    }
}