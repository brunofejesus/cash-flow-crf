using Crf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crf.Infrastructure.Persistence.Configurations
{
  public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
  {
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
      builder.ToTable("activity");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id)
        .HasColumnName("activity_id")
        .IsRequired();

      builder.Property(x => x.TypeActivity)
        .HasColumnName("type_activity")
        .IsRequired();

      builder.Property(x => x.AccountId)
        .HasColumnName("activity_account_id")
        .IsRequired();

      builder.Property(x => x.Created)
        .HasColumnName("activity_created")
        .IsRequired();

      builder.Property(x => x.Description)
        .HasColumnName("activity_description")
        .IsRequired();

      builder.HasOne(x => x.Account)
        .WithMany(x => x.Activities)
        .HasForeignKey(x => x.AccountId);
    }
  }
}