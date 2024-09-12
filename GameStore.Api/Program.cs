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
//app.UseRouting();
int? i = null;
app.Run();
var result = Calculator.Compare(5,6);
var str = Calculator.Compare("val","val1");

class Calculator
{
    public static bool Compare<T>(T x, T y)
    {
        return x.Equals(y);

        try
        {

        }
        finally
        {
            
        }
    }
}