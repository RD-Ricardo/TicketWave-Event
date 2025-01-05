using Api;
using CrossCutting.Dtos;
using Application.Events.CreateEvent;
using CrossCutting.Abstractions;
using Microsoft.AspNetCore.Mvc;

var app = Extensions.ConfigServer(args);

app.MapPost("/events", async (
    [FromServices] IUseCase<CreateEventInputModel, VoidResult> createEventUseCase,
    [FromBody] CreateEventInputModel input) =>
{
    var result = await createEventUseCase.Execute(input);

    return result.Match(onSuccess: value => Results.Created("", new { Data = value }),
                        onFailure: (errorDetails) => Errors.Get(errorDetails));
});

app.MapGet("/events", async ([FromServices] IUseCase<IEnumerable<EventDto>> getEventsUseCase) =>
{
    var result = await getEventsUseCase.Execute();
   
    return result.Match(onSuccess: value => Results.Ok(value),
                        onFailure: (errorDetails) => Errors.Get(errorDetails));
});

app.Run();

