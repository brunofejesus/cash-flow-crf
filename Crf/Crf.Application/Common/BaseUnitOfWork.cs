using Crf.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Crf.Application.Common
{
	public abstract class BaseUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, IDisposable
	{
		protected TContext? Context { get; private set; }

		protected IDbContextTransaction? Transaction { get; private set; }

		protected BaseUnitOfWork(TContext context)
		{
			Context = context;
		}

		public IDisposable BeginWork()
		{
			return Transaction ?? Context?.Database.BeginTransaction()!;
		}

		public int Commit(bool confirm = true)
		{
			int result = Context?.SaveChanges() ?? 0;
			if (confirm)
			{
				Transaction?.Commit();
			}
			return result;
		}

		public void Rollback()
		{
			Transaction?.Rollback();
		}

		public void Discard()
		{
			Transaction?.Dispose();
		}
	}
}