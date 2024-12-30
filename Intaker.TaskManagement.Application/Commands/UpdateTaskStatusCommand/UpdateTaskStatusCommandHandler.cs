using FluentValidation;
using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
    {
        private readonly ServiceBusHandler _serviceBusHandler;
        private readonly IValidator<UpdateTaskStatusCommand> _validator;

        public UpdateTaskStatusCommandHandler(ServiceBusHandler serviceBusHandler, IValidator<UpdateTaskStatusCommand> validator)
        {
            _serviceBusHandler = serviceBusHandler;
            _validator = validator;
        }

        public async Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            await _serviceBusHandler.SendMessage(QueueAction.UpdateTaskStatus, request);
        }
    }
}
