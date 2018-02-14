using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Azure.NotificationHubs;

namespace Gearstone.PushNotifications.WindowsAzure
{
    public class AzureNotificationService : INotificationService
    {
        NotificationHubClient hubClient;

        public AzureNotificationService(NotificationHubClient hubClient)
        {
            this.hubClient = hubClient;
        }

        public async Task SendTextNotification(string message, string destinationTag)
        {
            var templateValues = new Dictionary<string, string> { { "message", message } };

            await hubClient.SendTemplateNotificationAsync(templateValues, destinationTag + "&&text");
        }
    }
}