using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtension
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope(); // create scope
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>(); // get registered service created from program.cs
        await dbContext.Database.MigrateAsync(); //run the database migration
    }
}
