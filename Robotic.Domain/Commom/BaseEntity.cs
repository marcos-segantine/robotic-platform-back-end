namespace Robotic.Domain.Commom;

public class BaseEntity
{
    public DateTime CreatedOn { get; protected set; }
    public DateTime ModifiedOn { get; protected set; }

    public BaseEntity()
    {
        CreatedOn = DateTime.UtcNow;
        ModifiedOn = DateTime.UtcNow;
    }
}