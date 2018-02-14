using System.Collections.Generic;

namespace Gearstone.PushNotifications
{
    public interface IRegistrationService
    {
        void RegisterDevice(Platform platform, string pnsHandle, string deviceIdentifier, IList<string> tags);
    }
}