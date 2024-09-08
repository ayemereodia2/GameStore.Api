using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.GamesMapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GameEndPoints
{

    public static RouteGroupBuilder MapGameEndPoints(this WebApplication app)
    {
        const string GET_GAMES_ENDPOINT_NAME = "GetGame";
        
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) => 
           await dbContext.Games
                    .Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync()
        );
        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {

            Game? game = await dbContext.Games.FindAsync(id);
            return game == null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GET_GAMES_ENDPOINT_NAME);

        // POST /game
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext context) => 
        {
            Game game = newGame.toEntity();
            
            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();

            // convert to Dto to match contract
            return Results.CreatedAtRoute("GetGame", new {Id = game.Id}, game.ToGameDetailsDto());
        }
    );

        // PUT /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) => {
            Game? existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame == null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                      .CurrentValues
                      .SetValues(updateGame.toEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => {
            await dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
