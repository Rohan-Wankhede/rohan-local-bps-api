using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Users;

namespace DebugApi.Features;

internal class UserEfConfiguration : AggregateRootEfConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(user => user.UserName)
          .HasConversion(userName => userName.ToString(),
              userNameString => UserName.Create(userNameString!))
          .HasMaxLength(50)
          .IsRequired();

        builder.Property(user => user.SurName)
          .HasConversion(surName => surName.ToString(),
              surNameString => SurName.Create(surNameString!))
          .HasMaxLength(50)
          .IsRequired();

        builder.Property(user => user.UserEmail)
          .HasConversion(userEmail => userEmail.ToString(),
              userEmailString => UserEmail.Create(userEmailString!))
          .HasMaxLength(50)
          .IsRequired();

        builder.Property(user => user.UserRole)
          .HasConversion(userRole => userRole.ToString(),
              userRoleString => UserRole.Create(userRoleString!))
          .HasMaxLength(50)
          .IsRequired();

        builder.Property(user => user.UserStatus)
          .IsRequired();

    }
}
