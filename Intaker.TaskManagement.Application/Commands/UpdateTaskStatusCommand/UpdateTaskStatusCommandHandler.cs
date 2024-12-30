using Intaker.TaskManagement.Domain.Infrastructure;
using Intaker.TaskManagement.Infrastructure.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
    {
        private readonly IQueueService _queueService;

        public UpdateTaskStatusCommandHandler(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken) =>
            await _queueService.Enqueue(QueueAction.UpdateTaskStatus, request);
    }
}
