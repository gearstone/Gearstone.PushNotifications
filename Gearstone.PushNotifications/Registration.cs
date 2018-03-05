namespace Gearstone.PushNotifications
{
    public class Registration
    {
        /// <summary>
        /// A string that uniquely identifies this particular installation.
        /// </summary>
        public string DeviceIdentifier { get; set; }

        /// <summary>
        /// Which PNS platform the device was registered with.
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// The Push Notification Service handle that was received when the device
        /// registered with its native PNS.
        /// </summary>
        public string PnsHandle { get; set; }
    }
}
