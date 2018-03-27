### Registering devices

```c#
using Microsoft.Azure.NotificationHubs;
using Gearstone.PushNotifications;
using Gearstone.PushNotifications.WindowsAzure;

var hubConnection = "<listen only hub connection string>";
var hubName = "<hub name>";
var hubClient = NotificationHubClient.CreateClientFromConnectionString(hubConnection, hubName);

string chromePnsHandle = "<a handle from registering for Firebase notifications>";
var deviceIdentifier = "<a unique device identifier>";
var userIdentifier = "<an optional user identifier>";

// Register a device and tag it with who it belongs to
var regSvc = new AzureRegistrationService(hubClient);

var registration = new Registration
{
    DeviceIdentifier = deviceIdentifier,
    PnsHandle = chromePnsHandle,
    Platform = Platform.Google
};

regSvc.CreateRegistration(registration, new string[] { userIdentifier, deviceIdentifier });
```

### Sending notifications
```c#
using Microsoft.Azure.NotificationHubs;
using Gearstone.PushNotifications;
using Gearstone.PushNotifications.WindowsAzure;

var hubConnection = "<listen only hub connection string>";
var hubName = "<hub name>";
var hubClient = NotificationHubClient.CreateClientFromConnectionString(hubConnection, hubName);

var notifySvc = new AzureNotificationService(hubClient);
var destinationTag = "<tag>"; // Could be anything you've used as a tag, will go to all devices which have the tag
await notifySvc.SendTextNotification("Hello, Gearstone!", destinationTag);
```

#### Notes
1. The InstallationId can be any string you like, but should ideally be something unique to that particular installation.
1. Two registrations are created for each installation, one "Native" and one "Templated".
1. In addition to the tags you specify, several others are automatically added:
    - `$InstallationId{<id>}`: where `<id>` is the InstallationId used.
    - `text`: the name of the message template that the installation was created with. If more than one template was created, there will be a tag for each one.

    These can be used to target notifications just like any other tag.
1. It is possible to have multiple registrations for the same PNS Identifier. These can be queried using `HubClient.GetRegistrationsByChannelAsync` and old ones removed with `HubClient.

#### Quirks
1. Once you delete a registration using the Visual Stutio Azure Explorer, re-registering again using the same InstallationId won't create new registrations, presumably because the installation is still registered somewhere, but marked as deleted / hidden. However if you use `HubClient.DeleteInstallation`, you can then re-use the same InstallationId.

#### Glossary
`PNS Identifier`  
One of Azure's names for the unique handle you get when registering a device with it's native push notification service. Also called `Push Channel` in other parts of the Azure API. Apple calls these `Device Tokens` and Google calls them `Registration IDs`.

`Push Channel`  
Another name for `PNS Identifier`

`Installation`  
A meta concept that creates multiple device `registrations` and cleanly handles re-registration of the same device. Created and updated using `HubClient.CreateOrUpdateInstallation`.

`Registration`  
Used to record the `PNS Identifier` for a particular notficiation client along with which tags can be used to target it and which templates can be used to send to it. There can be multiple registrations for any given `PNS Identifier`. Using `Installations` makes it slightly easier to manage registrations rather than creating them directly.

`Templates`  
Each vendor's push notification service uses different data structures to specify a notification. Templates work around this issue by allowing you to specify a set of values to substitute into the correct places in each platform's data structure. Then you can just send a templated notification along with the desired values. Essentially to send a text message, you'd just create a template with a single variable called "message".

`Tags`  
Each registration can have a series of tags associated with it. These can be used to identify a particular user or class of users. Each registration can have up to 60 tags of 120 characters each. It is not possible to send a notification to a specific device unless, you tag it with a unique value and send a notification "all" the devices with that tag.