namespace TrainLine.Persistence.Extensions;

public static class PersistenceExtensions
{
	public static WebApplication EnsureDbExists(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();

		scope.ServiceProvider.GetRequiredService<Context>()
		.Database.EnsureCreated();

		return app;
	}
}