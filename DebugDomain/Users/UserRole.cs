using DebugDomain.Common;
using DebugDomain.Common.Extensions;
using DebugDomain.Employees.Exceptions;

namespace DebugDomain.Users;

public class UserRole : ValueObject
{
    private readonly string _userRole;

    private UserRole(string userRole) => _userRole = userRole;

    public static UserRole Create(string userRole)
    {
        Validate(userRole);

        return new UserRole(userRole.Trim());
    }

    private static void Validate(string userRole)
    {
        if (userRole.IsNullOrWhiteSpace())
            throw new InvalidEmployeeDescriptionException("UserRole can not be null or empty!");

        if (!userRole.HasValidLength(3, 200))
            throw new InvalidEmployeeDescriptionException("UserRole should be greater than or equal to 3 and less than or equal to 200 characters!");
    }

    public static implicit operator UserRole(string userRole) => Create(userRole);
    public static implicit operator string(UserRole userRole) => userRole.ToString();

    public override string ToString() => _userRole;
}
