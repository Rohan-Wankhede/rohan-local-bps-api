using DebugDomain.Common;
using DebugDomain.Employees.Events;

namespace DebugDomain.Employees;

public class Employee : AggregateRoot
{
    public EmployeeGrade Grade { get; private set; }

    public EmployeeTitle Title { get; private set; }

    public EmployeeName Name { get; private set; }

    public bool IsManager { get; private set; }

    private Employee(EmployeeGrade grade, EmployeeTitle title, EmployeeName name)
    {
        Grade = grade;
        Title = title;
        Name = name;
        IsManager = false;
    }

    public static Employee Create(EmployeeGrade grade, EmployeeTitle title, EmployeeName name)
    {
        var employee = new Employee(grade, title, name);

        employee.AddEvent(new EmployeeCreatedEvent(employee));

        return employee;
    }
}