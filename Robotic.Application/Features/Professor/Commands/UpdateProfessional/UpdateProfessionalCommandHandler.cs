using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Features.Professor.Commands.UpdateProfessional;

public class UpdateProfessionalCommandHandler
{
    private readonly IProfessionalRepository _repository;

    public UpdateProfessionalCommandHandler(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public void Handle(UpdateProfessionalCommand command)
    {
        var professional = new Professional(command.Id, command.Name, command.PhotoPath);
        
        _repository.Update(professional);
    }
}