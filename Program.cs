using System.Threading.Tasks;

using Microsoft.Azure.NotificationHubs;

using Gearstone.PushNotifications;
using Gearstone.PushNotifications.WindowsAzure;

namespace notifications
{
    class Program
    {
        static void Main(string[] args)
        {
            DoIt().GetAwaiter().GetResult();
        }

        static async Task DoIt()
        {
            var connectionString = @"Endpoint=sb://thnotify1.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Z2Xpa9dMYJLipI26LS1hltCZw8U4KI41NfnPucWkWqw=";
            var hub = NotificationHubClient.CreateClientFromConnectionString(connectionString, "not1");

            string chromePnsHandle = "APA91bEGv8n_2g_5wv1Cxj6Q9KumQZdLp8uoSzAtdhIJW27cGglow33CDHqpDFEXhae5dwRVN-m99W_tiHBriMmC5DDRxd_vR-uQPNiOdPDt2XK4qLtMMO2aJQlGq1wMihx-b7IcDZl528Ocx6I_x5ug4e6UVkPYbQ";

            var svc2 = new AzureRegistrationService(hub);
            svc2.RegisterDevice(Platform.Google, chromePnsHandle, "abcg", new string[] { "testuser", "ww" });

            var notify = new AzureNotificationService(hub);
            await notify.SendTextNotification("Hello, Gearstone!", "testuser");
        }
    }
}
