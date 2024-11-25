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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCustomServices();
        
        builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDb"));
        builder.Services.AddSingleton<ICosmosDbService>(provider =>
        {
            var connectionString = builder.Configuration.GetConnectionString("CosmosDb");
            var settings = provider.GetRequiredService<IOptions<CosmosDbSettings>>().Value;
            return new CosmosDbService(connectionString, settings);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors(policyBuilder =>
        {
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