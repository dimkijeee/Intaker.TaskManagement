using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
    {
        private readonly ServiceBusHandler _serviceBusHandler;

        public UpdateTaskStatusCommandHandler(ServiceBusHandler serviceBusHandler)
        {
            _serviceBusHandler = serviceBusHandler;
        }

        public async Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken) =>
            await _serviceBusHandler.SendMessage(QueueAction.UpdateTaskStatus, request);
    }
}
