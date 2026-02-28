using Api.Endpoints;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddItemServices();

var app = builder.Build();

app.UseExceptionHandler(exceptionApp =>
    exceptionApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = new
        {
            type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            title = "An error occurred while processing your request.",
            status = StatusCodes.Status500InternalServerError,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    }));

app.MapOpenApi();
app.MapItemEndpoints();

app.Run();

public partial class Program { }
