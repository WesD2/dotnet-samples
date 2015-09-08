using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Net.Http.Formatting;
using MongoRepository;

using System.Web.Http.Cors;

namespace pangramWebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "ActionApi",
            routeTemplate: "{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
    name: "Post API Default",
    routeTemplate: "{controller}",
    defaults: new { id = RouteParameter.Optional }
);

            //BsonClassMap.RegisterClassMap<Pangram>();
            //config.Formatters.Clear();                             //Remove all other formatters
            //config.Formatters.Add(new BsonMediaTypeFormatter());
        }
    }
}
