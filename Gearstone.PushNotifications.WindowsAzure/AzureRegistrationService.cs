using System;
using System.Collections.Generic;

using Microsoft.Azure.NotificationHubs;

namespace Gearstone.PushNotifications.WindowsAzure
{
    public class AzureRegistrationService : IRegistrationService
    {
        NotificationHubClient hubClient;

        public AzureRegistrationService(NotificationHubClient hubClient)
        {
            this.hubClient = hubClient;
        }

        public void CreateRegistration(string deviceIdentifier, Platform platform, string pnsHandle, IList<string> tags)
        {
            if (string.IsNullOrEmpty(pnsHandle)) throw new ArgumentException(nameof(pnsHandle));
            if (string.IsNullOrEmpty(deviceIdentifier)) throw new ArgumentException(nameof(deviceIdentifier));

            var install = new Installation
            {
                Platform = GetNotificationPlatform(platform),
                InstallationId = deviceIdentifier,
                PushChannel = pnsHandle,
                Tags = tags,
                Templates = GetTemplatesForPlatform(platform)
            };

            hubClient.CreateOrUpdateInstallation(install);
        }

        public void DeleteRegistration(string deviceIdentifier)
        {
            hubClient.DeleteInstallation(deviceIdentifier);
        }

        IDictionary<string, InstallationTemplate> GetTemplatesForPlatform(Platform platform)
        {
            switch (platform)
            {
                case Platform.Google:
                    return new Dictionary<string, InstallationTemplate>
                    {
                        { "text", new InstallationTemplate { Body = "{\"data\":{\"message\":\"$(message)\"}}" } }
                    };

                case Platform.Apple:
                    return new Dictionary<string, InstallationTemplate>
                    {
                        { "text", new InstallationTemplate { Body = "{\"aps\":{\"alert\":\"$(message)\"}}"} }
                    };
            }

            throw new NotSupportedException(platform.ToString());
        }

        NotificationPlatform GetNotificationPlatform(Platform platform)
        {
            switch (platform)
            {
                case Platform.Google: return NotificationPlatform.Gcm;
                case Platform.Apple: return NotificationPlatform.Apns;
            }

            throw new NotSupportedException(platform.ToString());
        }
    }
}