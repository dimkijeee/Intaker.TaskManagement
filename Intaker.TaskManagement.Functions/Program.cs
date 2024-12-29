using AutoMapper;
using Intaker.TaskManagement.Application.Dependencies;
using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Data;
using Intaker.TaskManagement.Data.Repositories;
using Intaker.TaskManagement.Domain.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.RegisterAutomapper();

        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IRepository<Intaker.TaskManagement.Data.Models.Task>, TaskRepository>();
        services.AddScoped(sp => new TaskActionsMessageHandler(
            sp.GetRequiredService<IRepository<Intaker.TaskManagement.Data.Models.Task>>(),
            sp.GetRequiredService<IMapper>()));
    })
    .Build();

host.Run();