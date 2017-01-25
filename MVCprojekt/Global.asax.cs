using MVCprojekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MVCprojekt.App_Start;

namespace MVCprojekt
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<ApplicationDbContext>(new IndentityDbInitializer());
            Application["Visited"] = 0;
        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["Visited"] = (int)Application["Visited"] + 1;
            Application.UnLock();
        }
    }
}

