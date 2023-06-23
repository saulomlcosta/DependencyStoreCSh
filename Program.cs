using DependencyStore;
using DependencyStore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterConfiguration();
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterSqlConnection(connStr);
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

var app = builder.Build();

app.MapControllers();

app.Run();