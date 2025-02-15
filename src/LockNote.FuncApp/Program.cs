using LockNote.Bl;
using LockNote.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
 builder.Services
     .AddApplicationInsightsTelemetryWorkerService()
     .ConfigureFunctionsApplicationInsights();

// set up services
 builder.Services.AddSingleton<ICosmosDbService>(_ =>
 {
     var connectionString = builder.Configuration.GetSection("COSMOS_DB_CONNECTION_STRING").Value;
     var dbName = builder.Configuration.GetSection("COSMOS_DB_NAME").Value;
     var containerName = builder.Configuration.GetSection("COSMOS_CON_NAME").Value;
     var settings = new CosmosDbSettings(){DatabaseName = dbName ?? "", ContainerName = containerName ?? ""};
     return new CosmosDbService(connectionString, settings);
 });

 builder.Services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
 builder.Services.AddScoped<NotesService>();

builder.Build().Run();