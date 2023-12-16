using Riok.Mapperly.Abstractions;
using TrainLine.Dtos;
using TrainLine.Persistence;

namespace TrainLine.Mapperly;

[Mapper]
public static partial class DrivesMapper
{
	public static partial GetDrive AsDto(this Drive drive);

	public static partial Drive AsEntity(this CreateDrive dto);
}