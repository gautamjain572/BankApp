using Microsoft.AspNetCore.Hosting;
using BankApp;

var builder = WebApplication.CreateBuilder(args);


// Add services from Startup.cs
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure middleware from Startup.cs
startup.Configure(app);

app.Run();
