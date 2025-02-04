using shoppinglist_backend;

var builder = WebApplication.CreateBuilder(args);

// Uten mellomrom og רזו
StaticConfiguration.Name = "DittNavn";

// Add services to the container.
builder.Services.AddSingleton<TableStorageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Dev",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();

bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

if (isDevelopment)
{
    app.UseCors("Dev");
}

app.MapGet("/api/shoppingitems", async (TableStorageService service) =>
{
    var items = await service.GetItemsAsync();
    return Results.Ok(items);
});

app.MapPost("/api/shoppingitems", async (TableStorageService service, ShoppingItem item) =>
{
    item.RowKey = Guid.NewGuid().ToString();
    await service.AddItemAsync(item);
    return Results.Created($"/api/shoppingitems/{item.RowKey}", item);
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();