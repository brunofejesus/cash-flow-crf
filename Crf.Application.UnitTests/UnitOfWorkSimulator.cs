using Crf.Application.Common.Interfaces;

namespace Crf.Application.UnitTests
{
	public class UnitOfWorkSimulator : IUnitOfWork
	{
		public bool CalledBeginWork { get; private set; }
		public bool CalledCommit { get; private set; }
		public bool CalledDiscard { get; private set; }
		public bool CalledRollback { get; private set; }

		public IDisposable BeginWork()
		{
			CalledBeginWork = true;
			return null!;
		}

		public int Commit(bool confirm = true)
		{
			CalledCommit = true;
			return 0;
		}

		public void Discard()
		{
			CalledDiscard = true;
		}

		public void Rollback()
		{
			CalledRollback = true;
		}
	}
}