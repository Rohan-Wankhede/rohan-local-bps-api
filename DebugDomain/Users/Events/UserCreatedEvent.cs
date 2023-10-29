using DebugDomain.Common;
using DebugDomain.Users;

namespace DebugDomain.Employees.Events;

public class UserCreatedEvent : DomainEvent
{
    public User User { get; }

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}
public class UserUpdatedEvent : DomainEvent
{
    public User User { get; }

    public UserUpdatedEvent(User user)
    {
        User = user;
    }
}
