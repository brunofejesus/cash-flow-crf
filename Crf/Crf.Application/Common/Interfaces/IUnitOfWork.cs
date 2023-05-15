namespace Crf.Application.Common.Interfaces
{
	public interface IUnitOfWork
	{
		IDisposable BeginWork();

		int Commit(bool confirm = true);

		void Rollback();

		void Discard();
	}
}