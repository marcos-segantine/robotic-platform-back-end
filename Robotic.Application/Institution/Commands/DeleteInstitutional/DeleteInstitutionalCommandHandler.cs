using Robotic.Application.Institution.Commands.DeleteInstitutional;
using Robotic.Application.Interfaces;

namespace Robotic.Application.Institution.Commands;

public class DeleteInstitutionalCommandHandler
{
    private readonly IInstitutionalRepository _repository;

    public DeleteInstitutionalCommandHandler(IInstitutionalRepository repository)
    {
        _repository = repository;
    }

    public void Handle(DeleteInstitutionalCommand command)
    {
        _repository.Delete(command.Id);
    }
}