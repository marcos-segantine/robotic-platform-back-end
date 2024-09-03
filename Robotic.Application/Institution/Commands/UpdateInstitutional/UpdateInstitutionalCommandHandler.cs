using Robotic.Application.Institution.Commands.UpdateInstitutional;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;

namespace Robotic.Application.Institution.Commands;

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