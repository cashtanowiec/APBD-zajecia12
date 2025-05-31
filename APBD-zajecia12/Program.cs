using APBD_zajecia12.Models;
using APBD_zajecia12.Services.Client;
using APBD_zajecia12.Services.Trips;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<ITripsService, TripsService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddDbContext<_2019sbdContext>();


var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();