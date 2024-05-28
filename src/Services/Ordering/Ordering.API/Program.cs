using Ordering.API.DI;
using Ordering.Application.DI;
using Ordering.Infrastructure.Data.Extentions;
using Ordering.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddApplicationServices(builder.Configuration).
    AddInfrastructureServices(builder.Configuration).
    AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitaliseDatabaseAsync();
}

app.Run();
