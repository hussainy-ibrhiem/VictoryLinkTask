﻿using Owin;
using System.Web.Http;

namespace HandlingService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = ConfigureApi();
            app.Use(config);
        }

        private HttpConfiguration ConfigureApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }
    }
}