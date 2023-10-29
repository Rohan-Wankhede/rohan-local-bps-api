using DebugDomain.Common;

namespace DebugDomain.Employees.Events;

public class EmployeeCreatedEvent : DomainEvent
{
    public Employee Employee { get; }

    public EmployeeCreatedEvent(Employee employee)
    {
        Employee = employee;
    }
}
