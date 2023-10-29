using DebugDomain.Common;
using DebugDomain.Common.Extensions;
using DebugDomain.Employees.Exceptions;

namespace DebugDomain.Users;

public class UserName : ValueObject
{
    private readonly string _userName;

    private UserName(string userName) => _userName = userName;

    public static UserName Create(string userName)
    {
        Validate(userName);

        return new UserName(userName.Trim());
    }

    private static void Validate(string userName)
    {
        if (userName.IsNullOrWhiteSpace())
            throw new InvalidEmployeeDescriptionException("UserName can not be null or empty!");

        if (!userName.HasValidLength(3, 200))
            throw new InvalidEmployeeDescriptionException("UserName should be greater than or equal to 3 and less than or equal to 200 characters!");
    }

    public static implicit operator UserName(string userName) => Create(userName);
    public static implicit operator string(UserName userName) => userName.ToString();

    public override string ToString() => _userName;
}
