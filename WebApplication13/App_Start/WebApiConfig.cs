using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication13
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
                routeTemplate: "api/{controller}/{action}/{id}/{id1}",
                defaults: new { id = RouteParameter.Optional, id1= RouteParameter.Optional }
            );
                
            config.Routes.MapHttpRoute(
        name: "ControllerAndAction",
        routeTemplate: "api/{controller}/{action}/{Email}/{password}",
        defaults: new { controller = "Userappsapi", action = "Postlogin" }
);
        }
    }
}
