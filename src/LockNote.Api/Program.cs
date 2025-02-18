using LockNote.Data;
using Microsoft.Extensions.Options;

namespace LockNote;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCustomServices();

        builder.Services.AddSingleton<ICosmosDbService>(_ =>
        {
            var connectionString = builder.Configuration.GetSection("COSMOS_DB_CONNECTION_STRING").Value;
            var dbName = builder.Configuration.GetSection("COSMOS_DB_NAME").Value;
            var containerName = builder.Configuration.GetSection("COSMOS_CON_NAME").Value;
            if (dbName == null || containerName == null)
            {
                throw new ArgumentException("Database name and container name are required");
            }

            var settings = new CosmosDbSettings(DatabaseName: dbName, containerName);
            return new CosmosDbService(connectionString, settings);
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(policyBuilder =>
        {
            // TODO: fix hardcoded ip
            policyBuilder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}