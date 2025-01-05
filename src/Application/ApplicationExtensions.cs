using Application.Events.CreateEvent;
using Application.UseCases.GetEvents;
using CrossCutting.Abstractions;
using CrossCutting.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<CreateEventInputModel, VoidResult>, CreateEventUseCase>();
            services.AddScoped<IUseCase<IEnumerable<EventDto>>, GetEventsUserCase>();
            return services;
        }
    }
}
