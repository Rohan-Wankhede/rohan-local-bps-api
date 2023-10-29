using System.Text.Json.Serialization;
using DebugDomain.Common;
using DebugDomain.Employees.Events;

namespace DebugDomain.Users;

public class User : AggregateRoot
{
    public UserName UserName { get; private set; }

    public UserEmail UserEmail { get; private set; }

    public UserRole UserRole { get; private set; }

    public UserStatus UserStatus { get; private set; }

    public SurName SurName { get; private set; }
    [JsonConstructor]
    private User(UserName userName, SurName surName, UserEmail userEmail, UserRole userRole, UserStatus userStatus)
    {
        UserName = userName;
        SurName = surName;
        UserEmail = userEmail;
        UserRole = userRole;
        UserStatus = userStatus;
    }

    public static User Create(UserName userName, SurName surName, UserEmail userEmail, UserRole userRole, UserStatus userStatus)
    {
        var user = new User(userName, surName, userEmail, userRole, userStatus);

        user.AddEvent(new UserCreatedEvent(user));

        return user;
    }
    
}
//public static User Update(UserName name, SurName surName, UserEmail email, UserRole role, UserStatus status)
//{
//    var user = new User(name, surName, email, role, status);

//    user.AddEvent(new UserUpdatedEvent(user));

//    return user;
//}