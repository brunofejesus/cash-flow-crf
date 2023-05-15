using Crf.Domain.Entities;
using Crf.Domain.Enums;
using Crf.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Crf.Infrastructure.Persistence
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Activity> Activities => Set<Activity>();

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.HasPostgresEnum<ETypeActivity>();
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      base.OnModelCreating(builder);
    }
  }
}