using Microsoft.EntityFrameworkCore;

namespace TrainLine.Persistence;

public class Context : DbContext
{
	public DbSet<Drive> Drives { get; set; } = null!;

	public Context(DbContextOptions<Context> options) : base(options) {}
}