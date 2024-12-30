using Intaker.TaskManagement.Domain.Infrastructure;
using Intaker.TaskManagement.Infrastructure.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.CreateTaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IQueueService _queueService;

        public CreateTaskCommandHandler(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken) =>
            await _queueService.Enqueue(QueueAction.CreateTask, request);
    }
}
