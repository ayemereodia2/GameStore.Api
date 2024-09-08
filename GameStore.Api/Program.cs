using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlServer<GameStoreContext>(connectionString);

var app = builder.Build();
app.MapGameEndPoints();
app.MapGenreEndpoint();
await app.MigrateDbAsync();
app.Run();
