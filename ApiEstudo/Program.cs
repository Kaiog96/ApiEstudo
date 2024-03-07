using ApiEstudo.Business;
using ApiEstudo.Business.Implementations;
using ApiEstudo.Model.Context;
using ApiEstudo.Repository;
using ApiEstudo.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3, 0))));

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
