using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/users/count",
                async (CoCreateDbContext db, CancellationToken ct) =>
                {
                    var count = await db.Users.CountAsync(ct);
                    return Results.Ok(new CountResponse(count));
                }
            )
            .WithName("GetUsersCount");

        return group;
    }

    public record CountResponse(int count);
}
