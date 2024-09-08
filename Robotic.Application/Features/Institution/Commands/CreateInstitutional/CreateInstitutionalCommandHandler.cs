using Robotic.Application.Features.Institution.Commands.CreateInstitutional;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Features.Institution.Commands;

public class CreateInstitutionalCommandHandler
{
    private readonly IInstitutionalRepository _repository;

    public CreateInstitutionalCommandHandler(IInstitutionalRepository repository)
    {
        _repository = repository;
    }

    public void Handle(CreateInstitutionalCommand command)
    {
        var institutional = new Institutional(command.Id, command.Name, command.PhotoPath);
        
        _repository.Create(institutional);
    }
}