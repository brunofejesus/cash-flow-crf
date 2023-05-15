using Crf.Application.Common;
using Crf.Infrastructure.Persistence;

namespace Crf.Infrastructure.Repositories
{
	public class UnitOfWork : BaseUnitOfWork<ApplicationDbContext>
	{
		public UnitOfWork(ApplicationDbContext context) : base(context)
		{
		}
	}
}