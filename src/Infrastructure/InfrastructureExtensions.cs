using Infrastructure.Persistence.MongoContext;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.AddScoped<IMongoDbContext, MongoDbContext>();
            return services;
        }
    }
}
