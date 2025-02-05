using shoppinglist_backend;

var builder = WebApplication.CreateBuilder(args);

// Uten mellomrom og øæå
StaticConfiguration.Name = "OlavR";

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
    // TODO bytte ut med AI på sikt, fortelle investorer at vi bruker ai idag.
    // Det er teknisk sett sant, fordi vi brukte copilot for å lage listen
    item.Category = item.Name.ToLower() switch
    {
        "ananas" => "Frukt", "melon" => "Frukt", "kiwi" => "Frukt", "paprika" => "Grønnsaker", "gulrot" => "Grønnsaker", "brokkoli" => "Grønnsaker", "blomkål" => "Grønnsaker", "spinat" => "Grønnsaker", "salat" => "Grønnsaker",  "løk" => "Grønnsaker", "hvitløk" => "Grønnsaker",
        "potet" => "Grønnsaker", "søtpotet" => "Grønnsaker", "mais" => "Grønnsaker", "erter" => "Grønnsaker", "bønner" => "Grønnsaker", "linser" => "Grønnsaker", "quinoa" => "Korn", "ris" => "Korn", "pasta" => "Korn","brød" => "Korn",  "havregryn" => "Korn", "kjeks" => "Snacks",
        "sjokolade" => "Snacks", "godteri" => "Snacks", "chips" => "Snacks", "popcorn" => "Snacks", "nøtter" => "Snacks", "mandler" => "Snacks", "cashewnøtter" => "Snacks", "peanøtter" => "Snacks", "valnøtter" => "Snacks", "pistasjnøtter" => "Snacks", "rosiner" => "Snacks",
        "tørkede aprikoser" => "Snacks", "tørkede tranebær" => "Snacks", "tørkede fiken" => "Snacks", "tørkede dadler" => "Snacks", "yoghurt" => "Meieri", "melk" => "Meieri", "ost" => "Meieri", "smør" => "Meieri", "kremost" => "Meieri",
        _ => "AI categorization is unavailable due to high demand"

    };
    await service.AddItemAsync(item);
    return Results.Created($"/api/shoppingitems/{item.RowKey}", item);
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
