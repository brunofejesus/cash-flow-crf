namespace Crf.Domain.Entities
{
	public class Account : BaseEntity
	{
		private readonly List<Activity> _activities = new();

		public string Name { get; init; }
		public string Number { get; init; }
		public string Bank { get; init; }
		public string UserId { get; init; }
		public IReadOnlyCollection<Activity> Activities => _activities.ToArray();

		public Account(string name, string number, string bank, string userId)
		{
			Name = name;
			Number = number;
			Bank = bank;
			UserId = userId;
		}

		public virtual void AddFinancialActivity(Activity activity)
		{
			if (activity is not null)
			{
				_activities.Add(activity);
			}
		}
	}
}