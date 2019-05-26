using System;
using System.Collections.Generic;
using System.Linq;
using LoggerService;
using System.Web.Http;

namespace TdtdAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            /***************************************************
             * To set CORS policy GLOBALY use:
             * 
             * var cors = new EnableCorsAttribute("www.example.com", "*", "*");
             * config.EnableCors(cors);
             * 
             * *************************************************/
            config.EnableCors();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
