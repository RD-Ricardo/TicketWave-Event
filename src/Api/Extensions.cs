using Application;
using CrossCutting.Abstractions;
using Infrastructure;

namespace Api
{
    public static class Extensions
    {
        public static WebApplication ConfigServer(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenApi();

            builder.Services
                .AddInfra()
                .AddApplication();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/openapi/v1.json", "My API V1");
                });
            }
            
            app.UseHttpsRedirection();
            
            return app;
        }
    }

    public static class Errors
    {
        public static IResult Get((ErrorType errorType, List<string>? erros) erroDetails)
        {
            var (errorType, erros) = erroDetails;

            var obj = new { Errors = erros, Type = errorType.ToString() };

            if (errorType == ErrorType.NotFound)
            {
                return Results.NotFound(obj);
            }
            else if (errorType == ErrorType.Invalid)
            {
                return Results.BadRequest(obj);
            }
            else if (errorType == ErrorType.Conflict)
            {
                return Results.Conflict(obj);
            }
            else if (errorType == ErrorType.InternalServerError)
            {
                return Results.InternalServerError(obj);
            }
            else
            {
                return Results.BadRequest(obj);
            }
        }
    }
}
