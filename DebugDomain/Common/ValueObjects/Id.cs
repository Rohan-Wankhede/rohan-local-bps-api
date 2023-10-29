using DebugDomain.Common.Exceptions;

namespace DebugDomain.Common.ValueObjects;

public sealed class Id : ValueObject
{
    public bool IsTransient => Value == Guid.Empty;

    public Guid Value { get; }

    public static Id Transient => new(Guid.Empty);

    private Id(Guid id) => Value = id;

    public static Id Create() => Create(Guid.NewGuid());

    public static Id Create(Guid id)
    {
        Validate(id);

        return new Id(id);
    }

    private static void Validate(Guid id)
    {
        if (id == Guid.Empty)
            throw new InvalidIdException(id.ToString());
    }

    public static implicit operator Guid(Id id) => id.Value;

    public static implicit operator Id(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}

