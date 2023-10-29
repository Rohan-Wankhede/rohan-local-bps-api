using DebugDomain.Common.Exceptions;

namespace DebugDomain.Employees.Exceptions;

public class InvalidEmployeeException : DomainException
{
    public override string Code => nameof(InvalidEmployeeException);

    public InvalidEmployeeException(string message) : base(message) { }
}