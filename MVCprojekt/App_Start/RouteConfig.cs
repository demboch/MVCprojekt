using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCprojekt
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "BronSzczegoly",
                url: "Bron-{id}",
                defaults: new { controller = "Bron", action = "Szczegoly" }
            );

            routes.MapRoute(
                name: "BronLista",
                url: "Model/{nazwa}",
                defaults: new { controller = "Bron", action = "Lista" },
                constraints: new { nawa = @"[\w]+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}