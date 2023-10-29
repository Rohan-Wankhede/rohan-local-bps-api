using System.Text.Json;
using System.Text.Json.Serialization;
using DebugDomain.Common.ValueObjects;

namespace DebugDomain.Common;

public abstract class Entity
{
    public Id Id { get; protected set; } = Id.Create();

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    private bool IsTransient() => Id.IsTransient;

    public override int GetHashCode() => Id.GetHashCode();
}
