using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainLine.Dtos;
using TrainLine.Mapperly;
using TrainLine.Persistence;
using TrainLine.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(x =>
{
	x.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

var app = builder.Build();

app.EnsureDbExists();

var group = app.MapGroup("/drives")
	.WithParameterValidation();

group.MapGet("", async (Context ctx, Microsoft.AspNetCore.Http.HttpContext httpCtx) =>
{
	var fromValue = httpCtx.Request.Query.FirstOrDefault(x => "from".Equals(x.Key, StringComparison.OrdinalIgnoreCase)).Value.FirstOrDefault();
	var toValue = httpCtx.Request.Query.FirstOrDefault(x => "to".Equals(x.Key, StringComparison.OrdinalIgnoreCase)).Value.FirstOrDefault();

	var drivesQuery = ctx.Drives.AsQueryable();

	if (!string.IsNullOrEmpty(fromValue))
	{
		drivesQuery = drivesQuery.Where(x => x.From.Equals(fromValue));
	}

	if (!string.IsNullOrEmpty(toValue))
	{
		drivesQuery = drivesQuery.Where(x => x.To.Equals(toValue));
	}

	return await drivesQuery
		.Select(x => x.AsDto())
		.ToListAsync();
});
group.MapGet("/{id}", async ([FromRoute]Guid id, Context ctx) =>
{
	var drive = await ctx.Drives.FindAsync(id);

	return drive is not null
		? Results.Ok(drive.AsDto())
		: Results.NotFound();
});
group.MapPost("", async (CreateDrive dto, Context ctx) =>
{
	var drive = dto.AsEntity();
	drive.Id = Guid.NewGuid();

	await ctx.Drives.AddAsync(drive);
	await ctx.SaveChangesAsync();

	return Results.Created($"/drives/{drive.Id}", drive);
});
group.MapPatch("/{id}", async ([FromRoute]Guid id,UpdateDrive dto, Context ctx) =>
{
	var drive = await ctx.Drives.FindAsync(id);

	if (drive is null) return Results.NotFound();

	drive.From = dto.From ?? drive.From;
	drive.To = dto.To ?? drive.To;
	drive.DepartureTime = dto.DepartureTime ?? drive.DepartureTime;
	drive.ArrivalTime = dto.ArrivalTime ?? drive.ArrivalTime;
	drive.PlacesLeft = dto.PlacesLeft ?? drive.PlacesLeft;
	drive.Price = dto.Price ?? drive.Price;

	await ctx.SaveChangesAsync();

	return Results.Ok(drive);
});
group.MapDelete("/{id}", async ([FromRoute]Guid id, Context ctx) =>
{
	var drive = await ctx.Drives.FindAsync(id);
	if (drive is null) return Results.NoContent();

	ctx.Drives.Remove(drive);
	await ctx.SaveChangesAsync();

	return Results.NoContent();
});

app.Run();