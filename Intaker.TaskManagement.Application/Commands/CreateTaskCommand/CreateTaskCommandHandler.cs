using FluentValidation;
using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.CreateTaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly ServiceBusHandler _serviceBusHandler;
        private readonly IValidator<CreateTaskCommand> _validator;

        public CreateTaskCommandHandler(ServiceBusHandler serviceBusHandler, IValidator<CreateTaskCommand> validator)
        {
            _serviceBusHandler = serviceBusHandler;
            _validator = validator;
        }

        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            await _serviceBusHandler.SendMessage(QueueAction.CreateTask, request);
        }
    }
}
