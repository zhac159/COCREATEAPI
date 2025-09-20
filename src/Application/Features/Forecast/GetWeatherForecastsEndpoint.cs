using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Forecast;

public static class GetweatherForecastsEndpoint
{
    public static RouteGroupBuilder MapWeatherEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/getWeatherForecast",
                async (IMediator mediator, int userid) =>
                {
                    var forecasts = await mediator.Send(new GetWeatherForecasts());
                    return Results.Ok(forecasts);
                }
            )
            .WithName("GetWeatherForecast");

        return group;
    }
}
