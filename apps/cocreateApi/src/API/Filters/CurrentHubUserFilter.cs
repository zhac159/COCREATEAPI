using Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API.Filters;

public class CurrentHubUserFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next
    )
    {
        var currentHubUser =
            invocationContext.ServiceProvider.GetRequiredService<ICurrentHubUser>();
        currentHubUser.SetContext(invocationContext.Context);
        return await next(invocationContext);
    }
}
