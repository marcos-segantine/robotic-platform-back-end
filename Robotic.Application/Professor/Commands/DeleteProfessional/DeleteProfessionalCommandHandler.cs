using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Professor.Commands.DeleteProfessional;

public class DeleteProfessionalCommandHandler
{
    private readonly IProfessionalRepository _repository;

    public DeleteProfessionalCommandHandler(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public void Handle(DeleteProfessionalCommand command)
    {
        _repository.Delete(command.Id);
    }
}