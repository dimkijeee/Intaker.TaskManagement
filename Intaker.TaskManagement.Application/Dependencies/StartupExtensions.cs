using Microsoft.Extensions.DependencyInjection;

namespace Intaker.TaskManagement.Application.Dependencies
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterRequestHandlers(
            this IServiceCollection services)
        {
            return services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(StartupExtensions).Assembly));
        }

        public static IServiceCollection RegisterAutomapper(
            this IServiceCollection services)
        {
            return services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
        }
    }
}
