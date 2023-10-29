using DebugDomain.Common.Exceptions;

namespace DebugDomain.Employees.Exceptions;

public class InvalidEmployeeDescriptionException : DomainException
{
    public override string Code => nameof(InvalidEmployeeDescriptionException);

    public InvalidEmployeeDescriptionException(string message) : base(message) { }
}