using System;
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
            var hubConnection = "Endpoint=sb://thnotify1.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Z2Xpa9dMYJLipI26LS1hltCZw8U4KI41NfnPucWkWqw=";
            var hubName = "not1";
            var hubClient = NotificationHubClient.CreateClientFromConnectionString(hubConnection, hubName);

            string chromePnsHandle = "APA91bEGv8n_2g_5wv1Cxj6Q9KumQZdLp8uoSzAtdhIJW27cGglow33CDHqpDFEXhae5dwRVN-m99W_tiHBriMmC5DDRxd_vR-uQPNiOdPDt2XK4qLtMMO2aJQlGq1wMihx-b7IcDZl528Ocx6I_x5ug4e6UVkPYbQ";

            // Register a device and tag it with who it belongs to
            var regSvc = new AzureRegistrationService(hubClient);
            var deviceIdentifier = "device4567";
            var userIdentifier = "user1234";
            regSvc.CreateRegistration(deviceIdentifier, Platform.Google, chromePnsHandle, new string[] { userIdentifier, deviceIdentifier });

            // Send a notification to a user (will go to all the devices they have registered on)
            var notifySvc = new AzureNotificationService(hubClient);
            await notifySvc.SendTextNotification("Hello, Gearstone!", userIdentifier);
        }
    }
}
