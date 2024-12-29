using Intaker.TaskManagement.Domain.Services;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
    {
        private readonly IQueueService _queueService;
        private readonly IRepository<Data.Models.Task> _repository;

        public UpdateTaskStatusCommandHandler(
            IQueueService queueService, 
            IRepository<Data.Models.Task> repository)
        {
            _queueService = queueService;
            _repository = repository;
        }

        public async Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var dbTask = await _repository.Get(request.Id);
            
            if (dbTask != null)
            {
                dbTask.Status = (int)request.NewStatus;
                
                await _repository.Update(dbTask);
                _repository.Save();
            }
        }
    }
}
