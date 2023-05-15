using Crf.Domain.Common.Interfaces;

namespace Crf.Domain.Commom
{
	public abstract class BaseEntity : IEntity
	{
		public int Id { get; private set; }
	}
}