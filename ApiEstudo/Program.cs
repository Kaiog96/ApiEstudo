using ApiEstudo.Model.Context;
using ApiEstudo.Services;
using ApiEstudo.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3, 0))));

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
