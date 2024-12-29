using Newtonsoft.Json;

namespace Intaker.TaskManagement.Infrastructure.Models
{
    public class QueueMessage
    {
        public QueueAction Action { get; set; }
        public object? Data { get; set; }

        public QueueMessage(QueueAction action, object data)
        {
            Action = action;
            Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
