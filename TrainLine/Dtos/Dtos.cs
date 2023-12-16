using System.ComponentModel.DataAnnotations;

namespace TrainLine.Dtos;

public record GetDrive(
	Guid Id,
	[MaxLength(200)]string From,
	[MaxLength(200)]string To,
	DateTime DepartureTime,
	DateTime ArrivalTime,
	[Range(0,int.MaxValue)] int PlacesLeft,
	[Range(0,int.MaxValue)]int Price
);

public record UpdateDrive(
	[MaxLength(200)]string? From,
	[MaxLength(200)]string? To,
	DateTime? DepartureTime,
	DateTime? ArrivalTime,
	[Range(0,int.MaxValue)]int? PlacesLeft,
	[Range(0,int.MaxValue)]int? Price
);

public record CreateDrive(
	[MaxLength(200)]string From,
	[MaxLength(200)]string To,
	DateTime DepartureTime,
	DateTime ArrivalTime,
	[Range(0,int.MaxValue)]int PlacesLeft,
	[Range(0,int.MaxValue)]int Price
);