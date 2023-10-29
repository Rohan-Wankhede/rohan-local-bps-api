using DebugDomain.Common;
using DebugDomain.Common.Extensions;
using DebugDomain.Employees.Exceptions;

namespace DebugDomain.Users;

public class UserEmail : ValueObject
{
    private readonly string _userEmail;

    private UserEmail(string userEmail) => _userEmail = userEmail;

    public static UserEmail Create(string userEmail)
    {
        Validate(userEmail);

        return new UserEmail(userEmail.Trim());
    }

    private static void Validate(string userEmail)
    {
        if (userEmail.IsNullOrWhiteSpace())
            throw new InvalidEmployeeDescriptionException("UserEmail can not be null or empty!");

        if (!userEmail.HasValidLength(3, 200))
            throw new InvalidEmployeeDescriptionException("UserEmail should be greater than or equal to 3 and less than or equal to 200 characters!");
    }

    public static implicit operator UserEmail(string userEmail) => Create(userEmail);
    public static implicit operator string(UserEmail userEmail) => userEmail.ToString();

    public override string ToString() => _userEmail;
}
