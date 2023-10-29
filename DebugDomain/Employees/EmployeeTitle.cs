using DebugDomain.Common;
using DebugDomain.Common.Extensions;
using DebugDomain.Employees.Exceptions;

namespace DebugDomain.Employees;

public class EmployeeTitle : ValueObject
{
    private readonly string _employeeTitle;

    private EmployeeTitle(string employeeTitle) => _employeeTitle = employeeTitle;

    public static EmployeeTitle Create(string employeeTitle)
    {
        Validate(employeeTitle);

        return new EmployeeTitle(employeeTitle.Trim());
    }

    private static void Validate(string employeeTitle)
    {
        if (employeeTitle.IsNullOrWhiteSpace())
            throw new InvalidEmployeeException("EmployeeTitle can not be null or empty!");

        if (!employeeTitle.HasValidLength(2, 51))
            throw new InvalidEmployeeException("EmployeeTitle should be greater than or equal to 2 and less than or equal to 51 characters!");
    }

    public static implicit operator EmployeeTitle(string employeeTitle) => Create(employeeTitle);
    public static implicit operator string(EmployeeTitle employeeTitle) => employeeTitle.ToString();

    public override string ToString() => _employeeTitle;
}
