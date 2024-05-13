using Ordering.API.DI;
using Ordering.Application.DI;
using Ordering.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddApplicationServices().
    AddInfrastructureServices(builder.Configuration).
    AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();
