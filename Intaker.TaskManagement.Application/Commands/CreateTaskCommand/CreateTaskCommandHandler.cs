using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.CreateTaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly ServiceBusHandler _serviceBusHandler;

        public CreateTaskCommandHandler(ServiceBusHandler serviceBusHandler)
        {
            _serviceBusHandler = serviceBusHandler;
        }

        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken) =>
            await _serviceBusHandler.SendMessage(QueueAction.CreateTask, request);
    }
}
