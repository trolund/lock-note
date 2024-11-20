using LockNote.Bl;
using LockNote.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace LockNote.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, NotesService notesService) : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        await notesService.CreateNoteAsync(new Note() { Id = Guid.NewGuid().ToString(), Content = "hej", CreatedAt = new DateTime(), IsDeleted = false});
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}