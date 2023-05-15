namespace Crf.Application.Common.Models
{
	public class Result
	{
		public bool Succeeded { get; init; }
		public List<string> Errors { get; init; }

		private Result(bool succeeded, IEnumerable<string> errors)
		{
			Succeeded = succeeded;
			Errors = errors.ToList();
		}

		public static Result Success()
		{
			return new Result(true, Array.Empty<string>());
		}

		public static Result Error(IEnumerable<string> errors)
		{
			return new Result(false, errors);
		}
	}
}