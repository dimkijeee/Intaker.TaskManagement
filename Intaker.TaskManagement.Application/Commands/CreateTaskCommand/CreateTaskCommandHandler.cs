using AutoMapper;
using Intaker.TaskManagement.Domain.Services;
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

        public Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
