using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
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



app.MapGet("/", () => "Hello World!");
app.MapGet("/pizzas/{id}", (int id) => Pizzas.GetPizza(id));
app.MapGet("/pizzas", () => Pizzas.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => Pizzas.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => Pizzas.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => Pizzas.RemovePizza(id));


app.Run();
