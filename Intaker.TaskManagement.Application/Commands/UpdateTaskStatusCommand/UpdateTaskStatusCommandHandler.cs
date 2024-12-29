using Intaker.TaskManagement.Domain.Services;
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

        public Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
