﻿using CSVtoJSON.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CSVtoJSON
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add a reference here to the new MediaTypeFormatter that adds text/plain support
            GlobalConfiguration.Configuration.Formatters.Insert(0, new TextMediaTypeFormatter());
        }
    }
}
