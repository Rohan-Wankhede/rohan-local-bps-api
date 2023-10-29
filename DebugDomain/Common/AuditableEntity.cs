namespace DebugDomain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? LastModifiedAtUtc { get; private set; }

    public void SetModificationDetails(string userId, DateTime utcDateTimeOfModification)
    {
        LastModifiedAtUtc = utcDateTimeOfModification;
    }

    public void SetCreationDetails(string userId, DateTime utcDateTimeOfCreation)
    {
        CreatedAtUtc = utcDateTimeOfCreation;
    }
}
