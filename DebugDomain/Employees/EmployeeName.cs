using DebugDomain.Common;
using DebugDomain.Common.Extensions;
using DebugDomain.Employees.Exceptions;

namespace DebugDomain.Employees;

public class EmployeeName : ValueObject
{
    private readonly string _employeeName;

    private EmployeeName(string employeeName) => _employeeName = employeeName;

    public static EmployeeName Create(string employeeName)
    {
        Validate(employeeName);

        return new EmployeeName(employeeName.Trim());
    }

    private static void Validate(string employeeDescription)
    {
        if (employeeDescription.IsNullOrWhiteSpace())
            throw new InvalidEmployeeDescriptionException("EmployeeName can not be null or empty!");

        if (!employeeDescription.HasValidLength(3, 200))
            throw new InvalidEmployeeDescriptionException("EmployeeName should be greater than or equal to 3 and less than or equal to 200 characters!");
    }

    public static implicit operator EmployeeName(string employeeName) => Create(employeeName);
    public static implicit operator string(EmployeeName employeeName) => employeeName.ToString();

    public override string ToString() => _employeeName;
}
