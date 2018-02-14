using System.Threading.Tasks;

namespace Gearstone.PushNotifications
{
    public interface INotificationService
    {
        Task SendTextNotification(string message, string destinationTag);
    }
}
