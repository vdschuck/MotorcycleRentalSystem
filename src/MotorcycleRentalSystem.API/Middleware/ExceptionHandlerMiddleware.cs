using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Extensions;
using MotorcycleRentalSystem.Responses;
using Npgsql;

namespace MotorcycleRentalSystem.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            logger.LogError(ex, "Record already exists database");
            await WriteErrorResponseAsync(context, HttpStatusCode.Conflict,
                AppMessage.DuplicateRecord.GetMessage());
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error saving to database");
            await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest,
                AppMessage.InvalidData.GetMessage());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception caught by middleware.");
            await WriteErrorResponseAsync(context, HttpStatusCode.InternalServerError,
                AppMessage.UnexpectedError.GetMessage());
        }
    }

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        return ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };
    }

    private async Task WriteErrorResponseAsync(HttpContext context, HttpStatusCode statusCode, string errorMessage)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var error = MessageResponse.From(errorMessage);
        await context.Response.WriteAsync(JsonSerializer.Serialize(error));
    }
}