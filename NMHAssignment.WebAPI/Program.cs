using NMHAssignment.Infrastructure.Persistance;
using NMHAssignment.Application;
using FluentValidation;
using System.Reflection;
using NMHAssignment.Infrastructure.Messaging.Configuration;
using NMHAssignment.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.Configure<MessageHubOptions>(builder.Configuration.GetSection("MessageHub"));

services.AddApplicationServices();
services.AddPersistance(builder.Configuration);
services.AddMessagingServices();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddControllers();

var app = builder.Build();

app.Services.ConnectToMessageHub();

//app.Services.ApplyMigration();

app.UseAuthorization();

app.MapControllers();

app.Run();
