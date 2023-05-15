using Crf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crf.Infrastructure.Persistence.Configurations
{
	public class AccountConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.ToTable("account");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnName("account_id")
				.IsRequired();

			builder.Property(x => x.Name)
				.HasColumnName("account_name")
				.IsRequired();

			builder.Property(x => x.Number)
				.HasColumnName("account_number")
				.IsRequired();

			builder.Property(x => x.Bank)
				.HasColumnName("account_bank")
				.IsRequired();

			builder.Property(x => x.UserId)
				.HasColumnName("account_user_id")
				.IsRequired();
		}
	}
}