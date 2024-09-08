using System;
using GameStore.Api.Data;
using GameStore.Api.GamesMapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenreEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("genres");
        group.MapGet("/", async (GameStoreContext dbContext) => 
        await dbContext.Genres
                       .Select(genere => genere.ToDto())
                       .AsNoTracking()
                       .ToListAsync());
        return group;
    }
}
