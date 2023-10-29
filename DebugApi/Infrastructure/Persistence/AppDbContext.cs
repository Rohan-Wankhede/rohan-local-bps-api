using Microsoft.EntityFrameworkCore;
using DebugDomain.Employees;
using DebugDomain.Users;
using System.Reflection;

namespace DebugApi.Infrastructure.Persistence;

public interface IAppDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
