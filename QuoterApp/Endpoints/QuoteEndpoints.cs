using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoterApp.Quoter;

namespace QuoterApp.Endpoints;

public static class QuoteEndpoints
{
    public static void MapQuoteEndpoints(this WebApplication app)
    {
        app.MapGet("/quote/{instrumentId}/quantity/{quantity:int}", ([FromRoute]string instrumentId, [FromRoute]int quantity,[FromServices] IQuoter quoter, [FromServices]ILogger logger) =>
        {
            try
            {
                return Results.Ok(quoter.GetQuote(instrumentId, quantity));
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to execute endpoint");
                return Results.Problem();
            }
        }).WithOpenApi();

        app.MapGet("/quote/{instrumentId}/vwap", ([FromRoute]string instrumentId, [FromServices] IQuoter quoter, [FromServices]ILogger logger) =>
        {
            try
            {
                return Results.Ok(quoter.GetVolumeWeightedAveragePrice(instrumentId));
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(e.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to execute endpoint");
                return Results.Problem();
            }
        }).WithOpenApi();
    }
}