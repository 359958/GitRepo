using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace WebAppMovie
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["UserID"] = "Guest";
            Session["CID"] = "1018";
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            Session["UserID"] = "";
            Session["CID"] = "";
            
        }
    }
    }
