using System.ComponentModel.DataAnnotations;

namespace TrainLine.Persistence;

public class Drive
{
	public Guid Id { get; set; }

	[MaxLength(200)]
	public string From { get; set; } = null!;

	[MaxLength(200)]
	public string To { get; set; } = null!;

	public DateTime DepartureTime { get; set; }

	public DateTime ArrivalTime { get; set; }

	[Range(0,int.MaxValue)]
	public int PlacesLeft { get; set; }

	[Range(0,int.MaxValue)]
	public int Price { get; set; }
}