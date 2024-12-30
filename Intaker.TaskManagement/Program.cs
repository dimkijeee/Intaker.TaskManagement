using AutoMapper;
using Azure.Messaging.ServiceBus;
using Intaker.TaskManagement.Application;
using Intaker.TaskManagement.Application.Dependencies;
using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Data;
using Intaker.TaskManagement.Data.Repositories;
using Intaker.TaskManagement.Domain.Infrastructure;
using Intaker.TaskManagement.Domain.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationContext>();

builder.Services.RegisterRequestHandlers();
builder.Services.RegisterAutomapper();

builder.Services.AddScoped<IRepository<Intaker.TaskManagement.Data.Models.Task>, TaskRepository>();

builder.Services.AddAzureClients(fb => fb.AddServiceBusClient(builder.Configuration["ServiceBusConnectionString"]));

builder.Services.AddScoped<IQueueService>(s => 
    new QueueService(
        s.GetRequiredService<ServiceBusClient>(), 
        builder.Configuration["QueueName"] ?? string.Empty));

builder.Services.AddScoped(sp => new TaskActionsMessageHandler(
    sp.GetRequiredService<IRepository<Intaker.TaskManagement.Data.Models.Task>>(),
    sp.GetRequiredService<IMapper>()));

builder.Services.AddScoped(s =>
    new ServiceBusHandler(
        s.GetRequiredService<TaskActionsMessageHandler>(),
        s.GetRequiredService<ServiceBusClient>(),
        builder.Configuration["QueueName"] ?? string.Empty));

builder.Services.AddHostedService<ServiceBusListener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
