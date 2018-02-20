using System.Collections.Generic;

namespace Gearstone.PushNotifications
{
    public interface IRegistrationService
    {
        void CreateRegistration(string deviceIdentifier, Platform platform, string pnsHandle, IList<string> tags);
        void DeleteRegistration(string deviceIdentifier);
    }
}