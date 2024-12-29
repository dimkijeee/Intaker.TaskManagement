using AutoMapper;
using Intaker.TaskManagement.Domain.Services;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.CreateTaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IQueueService _queueService;
        private readonly IRepository<Data.Models.Task> _repository;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(
            IQueueService queueService, 
            IRepository<Data.Models.Task> repository,
            IMapper mapper)
        {
            _queueService = queueService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var dbTask = _mapper.Map<Data.Models.Task>(request);

            await _repository.Create(dbTask);
            _repository.Save();
        }
    }
}
