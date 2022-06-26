var builder = WebApplication.CreateBuilder();

var app = builder.Build();

Console.WriteLine("Environment.ApplicationName: " + builder.Environment.ApplicationName);

Console.WriteLine("Environment.EnvironmentName: " + builder.Environment.EnvironmentName);



app.MapGet("/", () => "Hello World!");

app.Run();
