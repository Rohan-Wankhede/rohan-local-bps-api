using DebugDomain.Common;
using DebugDomain.Common.Extensions;
using DebugDomain.Employees.Exceptions;

namespace DebugDomain.Users;

public class SurName : ValueObject
{
    private readonly string _surName;

    private SurName(string surName) => _surName = surName;

    public static SurName Create(string surName)
    {
        Validate(surName);

        return new SurName(surName.Trim());
    }

    private static void Validate(string surName)
    {
        if (surName.IsNullOrWhiteSpace())
            throw new InvalidEmployeeDescriptionException("SurName can not be null or empty!");

        if (!surName.HasValidLength(3, 200))
            throw new InvalidEmployeeDescriptionException("SurName should be greater than or equal to 3 and less than or equal to 200 characters!");
    }

    public static implicit operator SurName(string surName) => Create(surName);
    public static implicit operator string(SurName surName) => surName.ToString();

    public override string ToString() => _surName;
}
