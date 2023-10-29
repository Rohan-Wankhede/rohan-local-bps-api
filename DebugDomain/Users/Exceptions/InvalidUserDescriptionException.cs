using DebugDomain.Common.Exceptions;

namespace DebugDomain.Employees.Exceptions;

public class InvalidUserDescriptionException : DomainException
{
    public override string Code => nameof(InvalidUserDescriptionException);

    public InvalidUserDescriptionException(string message) : base(message) { }
}