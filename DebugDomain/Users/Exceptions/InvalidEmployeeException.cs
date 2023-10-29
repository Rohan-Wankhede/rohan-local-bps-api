using DebugDomain.Common.Exceptions;

namespace DebugDomain.Employees.Exceptions;

public class InvalidUserException : DomainException
{
    public override string Code => nameof(InvalidUserException);

    public InvalidUserException(string message) : base(message) { }
}