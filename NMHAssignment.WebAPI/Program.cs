using NMHAssignment.Infrastructure.Persistance;
using NMHAssignment.Application;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

var app = builder.Build();

app.Services.ApplyMigration();

app.UseAuthorization();

app.MapControllers();

app.Run();
