# Microsoft tutorial
Best guide: https://scribe.bus-hit.me/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229  
(Skip step creating database with migrations)  
Microsoft reference:
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio

# To cover
Use above guide and walk through
- Creating a Web API project
- Using swagger to test the API
- Connecting to a database and updating database entries
- Dependency injection
- If time allows fetch api

# Api controllers
```csharp
[Route("api/[controller]")]   // /api/search
[ApiController]
public class SearchController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // Get: /api/search/test
    [HttpGet]
    [Route("test")]
    public string Test()
    {
        return "Testing";
    }

    // Get: /api/search/forecast?day=friday
    [HttpGet]
    [Route("Forecast")]
    public IEnumerable<Forecast> Forecast(string day)
    {
        if (!Enum.TryParse<DayOfWeek>(day, true, out var dayOfWeek))
        {
            throw new ArgumentException($"Day of week {day} is not recognised");
        }

        var rng = new Random();

        var weatherNext3Days = Enumerable.Range(1, 3).Select(x => Summaries[rng.Next(Summaries.Length)])
            .ToArray();

        var returnList = new List<Forecast>()
        {
            new Forecast(dayOfWeek, weatherNext3Days[0]),
            new Forecast((DayOfWeek) (((int) dayOfWeek + 1) % 7), weatherNext3Days[1]),
            new Forecast((DayOfWeek) (((int) dayOfWeek + 2) % 7), weatherNext3Days[2]),
        };

        return returnList;
    }
    
    
        [HttpPost]
        [Route("Update")]
        public string UpdateForecast([FromBody] Forecast forecast)
        {
            return $"The forecast you sent was {forecast.Weather} for {forecast.Day}";
        }

        [HttpPost]
        [Route("SimpleUpdate")]
        public string SimpleUpdateForecast(string forecast)
        {
            return $"The forecast you sent was {forecast}";
        }
}

public class Forecast 
{

    public Forecast()
    {

    }
    
    public Forecast(DayOfWeek day, string weather)
    {
        Day = day.ToString();
        Weather = weather;
    }
    public string Day { get; set; }
    public string Weather { get; set; }
}


```

## Swagger
Swashbuckle.AspNetCore
https://github.com/domaindrivendev/Swashbuckle.AspNetCore

Checkout the startup.cs file for what had to add for swagger.

Also need to make sure all controller methods have [Http...] e.g [HttpGet] annotations

# Js Ajax requests
Hitting the server as I like to call it.

### Http status codes
https://httpstatusdogs.com/

## Server request
### Fetch api
JS built in api for doing ajax requests

https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch
```js
fetch('/api/search/forecast?day=saturday')
   .then(response => response.json())
   .then(data => console.log(data));

```

### Axios
https://github.com/axios/axios
Popular libray for doing ajax requests

```js
axios.get('/api/search/forecast?day=today')
    .then(function (response) {
        debugger;
        console.log(response.data);
    })
    .catch(function (error) {
        // handle error
        console.log(error);
    });
    
    
axios.post('/api/search/update', { day: "Today", weather: "Sunny"})
    .then(function (response) {
        debugger;
        console.log(response.data);
    })
    .catch(function (error) {
        // handle error
        console.log(error);
    });
```

# Homework
