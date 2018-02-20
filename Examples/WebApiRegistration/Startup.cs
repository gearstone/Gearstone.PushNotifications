﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.Azure.NotificationHubs;

using Gearstone.PushNotifications;
using Gearstone.PushNotifications.WindowsAzure;

namespace RegistrationWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var hubConnection = "Endpoint=sb://thnotify1.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Z2Xpa9dMYJLipI26LS1hltCZw8U4KI41NfnPucWkWqw=";
            var hubName = "not1";
            var hubClient = NotificationHubClient.CreateClientFromConnectionString(hubConnection, hubName);
            var registrationSvc = new AzureRegistrationService(hubClient);
            services.AddSingleton<IRegistrationService>(registrationSvc);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
