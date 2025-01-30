using RectangleApi.Application.Services;
using RectangleApi.Domain.Repositories;
using RectangleApi.Domain.Services;
using RectangleApi.Infrastructure.Repositories;
using RectangleApi.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder
    .Services
    .AddOpenApi()
    .AddScoped<IRectangleService, RectangleService>()
    .AddSingleton<IRectangleValidationService, RectangleValidationService>()
    .AddScoped<IRectangleRepository>(x =>
        new RectangleFileRepository(
            x.GetRequiredService<ILogger<RectangleFileRepository>>(),
            "Resources/rectangle.json")
    );

builder.Services.AddLogging(cfg => cfg.AddConsole());

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        x => x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app
    .RegisterGetRectangleEndpoint()
    .RegisterPutRectangleEndpoint();

app.Run();