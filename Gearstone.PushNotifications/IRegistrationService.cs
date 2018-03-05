using System.Collections.Generic;

namespace Gearstone.PushNotifications
{
    public interface IRegistrationService
    {
        void CreateRegistration(Registration registration, IList<string> tags);
        void DeleteRegistration(string deviceIdentifier);
    }
}