using Robotic.Application.Interfaces;

namespace Robotic.Application.Features.Professor.Commands.DeleteProfessional;

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