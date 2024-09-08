using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Features.Institution.Commands.UpdateInstitutional;

public class UpdateInstitutionalCommandHandler
{
    private readonly IInstitutionalRepository _repository;

    public UpdateInstitutionalCommandHandler(IInstitutionalRepository repository)
    {
        _repository = repository;
    }

    public void Handle(UpdateInstitutionalCommand command)
    {
        var institutional = new Institutional(command.Id, command.Name, command.PhotoPath);
        
        _repository.Update(institutional);
    }
}