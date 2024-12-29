using AutoMapper;
using Intaker.TaskManagement.Application.Commands.CreateTaskCommand;
using Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand;
using Intaker.TaskManagement.Domain.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using Newtonsoft.Json.Linq;

namespace Intaker.TaskManagement.Application.Services
{
    public class TaskActionsMessageHandler
    {
        IRepository<Data.Models.Task> _repository;
        IMapper _mapper;

        public TaskActionsMessageHandler(IRepository<Data.Models.Task> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task HandleMessage(QueueMessage message)
        {
            try
            {
                switch (message.Action)
                {
                    case QueueAction.CreateTask:
                        await CreateTask((message.Data as JObject).ToObject<CreateTaskCommand>());
                        break;
                    case QueueAction.UpdateTaskStatus:
                        await UpdateTaskStatus((message.Data as JObject).ToObject<UpdateTaskStatusCommand>());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Action is not supported");
                }
            }
            catch
            {
                // TODO: send notification here
            }

            // TODO: send notification here
        }

        private async Task CreateTask(CreateTaskCommand command)
        {
            var dbTask = _mapper.Map<Data.Models.Task>(command);

            await _repository.Create(dbTask);
            _repository.Save();
        }

        private async Task UpdateTaskStatus(UpdateTaskStatusCommand command)
        {
            var dbTask = await _repository.Get(command.Id);

            if (dbTask != null)
            {
                dbTask.Status = (int)command.NewStatus;

                await _repository.Update(dbTask);
                _repository.Save();
            }
        }
    }
}
