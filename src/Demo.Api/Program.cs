using Demo.Api;
using Demo.Application;
using Demo.Infrastructure;
using Demo.Infrastructure.Data;
using Demo.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.UseServiceDefaults();

app.UseApiServices();

await app.RunAsync();
