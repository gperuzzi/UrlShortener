using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlShortener
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "", // URL with parameters
                new { controller = "UrlShortener", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Redirect", // Route name
                "{encodedId}", // URL with parameters
                new { controller = "UrlShortener", action = "RedirectUrl", id = UrlParameter.Optional } // Parameter defaults
);
        }
    }
}
