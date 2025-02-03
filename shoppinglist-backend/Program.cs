using shoppinglist_backend;

var builder = WebApplication.CreateBuilder(args);
// Uten mellomrom
StaticConfiguration.Name = "DittNavn";

// Add services to the container.
builder.Services.AddSingleton<TableStorageService>();

var app = builder.Build();

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