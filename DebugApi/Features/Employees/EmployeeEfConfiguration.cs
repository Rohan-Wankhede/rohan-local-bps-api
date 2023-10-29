using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Employees;

namespace DebugApi.Features;

internal class EmployeeEfConfiguration : AggregateRootEfConfiguration<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);

        builder.Property(employee => employee.Grade)
            .IsRequired();

        builder.Property(employee => employee.Title)
            .HasConversion(title => title.ToString(),
                titleString => EmployeeTitle.Create(titleString!))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(employee => employee.Name)
            .HasConversion(description => description.ToString(),
                descriptionString => EmployeeName.Create(descriptionString))
            .HasMaxLength(200)
            .IsRequired();
    }
}
