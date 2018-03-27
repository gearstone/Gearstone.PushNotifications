Experimental Web API registration endpoint for push notifications.

This exposes three /api endpoints:

POST `/api/login` - Obtain a JWT for authorisation

Request body:
```
{
    Username: "<username>",
    Password: "<password>"
}
```

Credentials are hardwired as `username` and `password`. The response will be a JSON object, and the `token` field will be a JWT token. This token should be used to authenticate requests to other endpoints by adding it to the `Authorization` header:

`Authorization: Bearer <token>`

---

POST `/api/registrations` - Register for push notifications

Request body:
```
{
    DeviceIdentifier: "<id>",
    Platform: "<platform>",
    PnsHandle: "<handle>"
}
```

`<id>` should uniquely identify the device that the app has been installed on. Ideally if the app is uninstalled and re-installed, the same identifier should be used.

`<platform>` must be either `Apple` or `Google` depending on the operating system of the device being registered.

`<handle>` is obtained when you register with the device's native push notification service.

---

DELETE `/api/registrations/<id>`

This should be used to delete an existing device registration when the app is uninstalled, if it is possible to detect such a thing.

`<id>` should be the Device Identifier which was used to register for notifications when the app was installed.