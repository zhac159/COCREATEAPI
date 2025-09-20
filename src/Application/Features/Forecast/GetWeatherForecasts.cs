using FluentValidation;
using Mediator;

namespace Application.Features.Forecast;

public sealed record GetWeatherForecasts : IQuery<IEnumerable<WeatherForecast>>
{
    public int UserId { get; init; }
}

public class GetWeatherForecastsValidator : AbstractValidator<GetWeatherForecasts>
{
    public GetWeatherForecastsValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0");
    }
}

public sealed record GetWeatherForecastsHandler
    : IQueryHandler<GetWeatherForecasts, IEnumerable<WeatherForecast>>
{
    public ValueTask<IEnumerable<WeatherForecast>> Handle(
        GetWeatherForecasts query,
        CancellationToken cancellationToken
    )
    {
        var result = Enumerable
            .Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "hello",
            })
            .ToArray();

        return new ValueTask<IEnumerable<WeatherForecast>>(result);
    }
}
