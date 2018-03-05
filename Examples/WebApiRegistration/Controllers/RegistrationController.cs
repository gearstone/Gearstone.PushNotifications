using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Gearstone.PushNotifications;

namespace RegistrationWebApi.Controllers
{
    [Authorize]
    [Route("api/registrations")]
    public class RegistrationController : Controller
    {
        IRegistrationService registrationSvc;

        public RegistrationController(IRegistrationService registrationSvc)
        {
            this.registrationSvc = registrationSvc;
        }

        [HttpPost]
        public void Post([FromBody]Registration model)
        {
            registrationSvc.CreateRegistration(model, new string[] { User.Claims.First(x => x.Type == ClaimTypes.Email).Value, model.DeviceIdentifier });
        }

        [HttpDelete]
        [Route("{installation}")]
        public void Delete(string installation)
        {
            registrationSvc.DeleteRegistration(installation);
        }
    }
}
