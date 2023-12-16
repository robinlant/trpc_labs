using TrainLine.Persistence;

namespace TrainLine.Specifications;

public class FromSpecification : ISpecification<Drive>
{
	private readonly string? _from;

	public FromSpecification(string? from)
	{
		_from = from;
	}

	public bool IsSatisfiedBy(Drive item) => string.IsNullOrEmpty(_from) || item.From.Equals(_from, StringComparison.OrdinalIgnoreCase);
}