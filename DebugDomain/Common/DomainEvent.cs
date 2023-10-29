using DebugDomain.Common.ValueObjects;

namespace DebugDomain.Common;

public abstract class DomainEvent
{
    public Id Id { get; } = Id.Create();

    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
