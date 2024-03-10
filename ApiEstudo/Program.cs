using ApiEstudo.Business;
using ApiEstudo.Business.Implementations;
using ApiEstudo.Hypermedia.Enricher;
using ApiEstudo.Hypermedia.Filters;
using ApiEstudo.Model.Context;
using ApiEstudo.Repository;
using ApiEstudo.Repository.Generic;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3, 0))));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof (IRepository<>), typeof(GenericRepository<>));

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

builder.Services.AddSingleton(filterOptions);	

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

app.Run();

void MigrateDatabase(string connection)
{
	try
	{
		var evolveConnection = new MySqlConnection(connection);

		var evolve = new Evolve(evolveConnection, Log.Information)
		{
			Locations = new List<string> { "db/migrations", "db/dataset" },
			IsEraseDisabled = true,
		};
		evolve.Migrate();
	}
	catch (Exception ex)
	{
		Log.Error("Database migration failed", ex);
		throw;
	}
}