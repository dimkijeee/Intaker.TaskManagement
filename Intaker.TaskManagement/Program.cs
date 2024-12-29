using Intaker.TaskManagement.Application.Dependencies;
using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Data;
using Intaker.TaskManagement.Data.Repositories;
using Intaker.TaskManagement.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationContext>();

builder.Services.RegisterRequestHandlers();
builder.Services.RegisterAutomapper();

builder.Services.AddScoped<IRepository<Intaker.TaskManagement.Data.Models.Task>, TaskRepository>();
builder.Services.AddScoped<IQueueService, QueueService>();

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
