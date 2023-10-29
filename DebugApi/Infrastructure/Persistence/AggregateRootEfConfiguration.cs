using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DebugDomain.Common;
using DebugDomain.Common.ValueObjects;

namespace DebugApi.Infrastructure.Persistence;

internal class AggregateRootEfConfiguration<T> : IEntityTypeConfiguration<T> where T : AggregateRoot
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(entity => entity.Id)
            .HasConversion(id => id.IsTransient ? Id.Create().Value : id.Value,
                idString => idString);

        // Add explanation about RowVersion
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion();

        builder.Ignore(employee => employee.DomainEvents);
        builder.Ignore(user => user.DomainEvents);
    }
}

