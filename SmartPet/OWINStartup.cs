using System;
using System.Collections.Generic;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SmartPet.OWINStartupConfig))]

namespace SmartPet
{
    public class OWINStartupConfig
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("SmartPetConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}