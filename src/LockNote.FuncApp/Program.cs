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
 builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDb"));
 builder.Services.AddSingleton<ICosmosDbService>(provider =>
 {
     var connectionString = builder.Configuration.GetSection("COSMOS_DB_CONNECTION_STRING").Value;
     var settings = provider.GetRequiredService<IOptions<CosmosDbSettings>>().Value;
     return new CosmosDbService(connectionString, settings);
 });

builder.Build().Run();