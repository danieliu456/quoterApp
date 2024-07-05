using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using QuoterApp.Endpoints;
using QuoterApp.MarketOrderConsumer;
using QuoterApp.MarketOrderPublisher;
using QuoterApp.Quoter;
using QuoterApp.Storage;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Quoter API",
    });
});

builder.Services.AddTransient<IQuoter, YourQuoter>();
builder.Services.AddTransient<IMarketOrderSource, HardcodedMarketOrderSource>();
builder.Services.AddSingleton<IInMemoryRepository, InMemoryRepository>();

builder.Services.AddMassTransit(
        x =>
        {
            x.UsingInMemory((context,cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
            x.AddConsumer<MarketOrderConsumer>();
        }
    );

builder.Services.AddHostedService<QuotePublisher>();


var app = builder.Build();

app.MapQuoteEndpoints();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();




