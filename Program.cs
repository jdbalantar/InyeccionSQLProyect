using InyeccionSQLProyect.DataContext;
using InyeccionSQLProyect.Services.Oracle;
using InyeccionSQLProyect.Services.Postgres;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OracleContext>(x => x.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));
builder.Services.AddDbContext<PgContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddTransient<IOUsersServices, OUsersServices>();
builder.Services.AddTransient<IPUsersServices, PUsersServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
