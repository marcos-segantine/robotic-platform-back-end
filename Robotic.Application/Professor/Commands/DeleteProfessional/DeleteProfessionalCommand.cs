namespace Robotic.Application.Professor.Commands.DeleteProfessional;

public class DeleteProfessionalCommand
{
    public Guid Id { get; private set; }
    
    public DeleteProfessionalCommand(Guid id)
    {
        Id = id;
    }   
}