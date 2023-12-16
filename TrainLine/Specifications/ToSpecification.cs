using TrainLine.Persistence;

namespace TrainLine.Specifications;

public class ToSpecification : ISpecification<Drive>
{
	private readonly string? _to;

	public ToSpecification(string? to)
	{
		_to = to;
	}

	public bool IsSatisfiedBy(Drive item) => string.IsNullOrEmpty(_to) || item.To.Equals(_to, StringComparison.OrdinalIgnoreCase);
}