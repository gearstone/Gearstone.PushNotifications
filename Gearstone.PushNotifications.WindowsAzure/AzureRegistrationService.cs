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

        public void CreateRegistration(Registration registration, IList<string> tags)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            CreateRegistration(registration.DeviceIdentifier, registration.Platform, registration.PnsHandle, tags);
        }

        public void DeleteRegistration(string deviceIdentifier)
        {
            hubClient.DeleteInstallation(deviceIdentifier);
        }

        void CreateRegistration(string deviceIdentifier, Platform platform, string pnsHandle, IList<string> tags)
        {
            if (string.IsNullOrEmpty(pnsHandle)) throw new ArgumentException(nameof(pnsHandle));
            if (string.IsNullOrEmpty(deviceIdentifier)) throw new ArgumentException(nameof(deviceIdentifier));

            var install = new Installation
            {
                Platform = GetNotificationPlatform(platform),
                InstallationId = deviceIdentifier,
                PushChannel = pnsHandle,
                Tags = tags ?? new List<string>(),
                Templates = GetTemplatesForPlatform(platform)
            };

            hubClient.CreateOrUpdateInstallation(install);
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