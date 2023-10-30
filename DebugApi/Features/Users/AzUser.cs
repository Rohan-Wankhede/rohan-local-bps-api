using DebugDomain.Users;

namespace DebugApi.Features.Users;

public class AzUser
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? SurName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserRole { get; set; }
    public UserStatus UserStatus { get; set; }

  
}

