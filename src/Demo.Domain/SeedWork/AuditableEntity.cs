namespace Demo.Domain.SeedWork;

public abstract class AuditableEntity : Entity
{
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? LastModifiedAt { get; protected set; }

    public void MarkAsCreated(DateTimeOffset now)
    {
        CreatedAt = now;
    }

    public void MarkAsModified(DateTimeOffset now)
    {
        LastModifiedAt = now;
    }
}
