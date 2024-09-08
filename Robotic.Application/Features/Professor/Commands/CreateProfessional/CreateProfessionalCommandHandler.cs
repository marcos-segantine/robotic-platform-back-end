using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Features.Professor.Commands.CreateProfessional;

public class CreateProfessionalCommandHandler
{
    private readonly IProfessionalRepository _repository;

    public CreateProfessionalCommandHandler(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public void Handle(CreateProfessionalCommand command)
    {
        var professional = new Professional(command.Id, command.Name, command.PhotoPath);
        
        _repository.Create(professional);
    }
}