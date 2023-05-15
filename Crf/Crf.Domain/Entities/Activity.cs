using Crf.Domain.Enums;

namespace Crf.Domain.Entities
{
	public class Activity : BaseEntity
	{
		public int AccountId { get; init; }
		public ETypeActivity TypeActivity { get; init; }
		public string Description { get; init; }
		public decimal Amount { get; init; }
		public DateTime Created { get; init; }
		public Account? Account { get; set; }

		protected Activity(int accountId, ETypeActivity typeActivity, string description, decimal amount, DateTime created, Account? account)
		{
			AccountId = accountId;
			TypeActivity = typeActivity;
			Description = description;
			Amount = amount;
			Created = created;
			Account = account;
		}

		public Activity(ETypeActivity typeActivity, string description, decimal amount)
		{
			TypeActivity = typeActivity;
			Description = description;
			Amount = amount;
			Created = DateTime.UtcNow;
		}
	}
}