using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Intaker.TaskManagement.Infrastructure.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IEnumerable<Uri> _notifyUrls;

        public NotificationService(IEnumerable<Uri> notifyUrls)
        {
            _notifyUrls = notifyUrls;
        }

        public async Task Notify(string message)
        {
            using var client = new HttpClient();

            foreach (Uri notifyUrl in _notifyUrls)
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = notifyUrl,
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonConvert.SerializeObject(
                        new 
                        { 
                            Message = message 
                        }))
                };

                await client.SendAsync(request);
            }
        }
    }
}
