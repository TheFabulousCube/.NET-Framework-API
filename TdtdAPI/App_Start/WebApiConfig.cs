using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

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
            var cors = new EnableCorsAttribute("*", "*", "*");
            //var cors = new EnableCorsAttribute("http://localhost:4200, http://staging.tadaatiedye.com, http://admin.tadaatiedye.com", "*", "*");
            config.EnableCors(cors);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /*****************************************************************************************
            // Instead: SwaggerConfig.Register(config);
            *****************************************************************************************/
            //config.EnableSwagger(c =>
            //{
            //    c.SingleApiVersion("v1", "TdtdApi");
            //    c.PrettyPrint();
            //    c.IncludeXmlComments(String.Format(@"{0}\App_Data\Swagger.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            //})
            //.EnableSwaggerUi();

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
