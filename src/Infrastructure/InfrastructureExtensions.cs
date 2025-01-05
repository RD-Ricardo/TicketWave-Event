using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.MongoContext;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.AddTransient<IMongoDbContext, MongoDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
