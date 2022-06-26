using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSqlite<PizzaDb>(connectionString);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "PizzaStore API",      
        Description = "Making the Pizzas you love", 
        Version = "v1"});
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

Console.WriteLine("Environment.ApplicationName: " + builder.Environment.ApplicationName);

Console.WriteLine("Environment.EnvironmentName: " + builder.Environment.EnvironmentName);

app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());

app.MapGet("/pizza/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));

app.MapPost("/pizza", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatePizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updatePizza.Name;
    pizza.Description = updatePizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
    {
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/", () => "Hello World!");
//app.MapGet("/pizzas/{id}", (int id) => Pizzas.GetPizza(id));
//app.MapGet("/pizzas", () => Pizzas.GetPizzas());
//app.MapPost("/pizzas", (Pizza pizza) => Pizzas.CreatePizza(pizza));
//app.MapPut("/pizzas", (Pizza pizza) => Pizzas.UpdatePizza(pizza));
//app.MapDelete("/pizzas/{id}", (int id) => Pizzas.RemovePizza(id));


app.Run();
