using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using AssetFlow.Legacy.Web.App_Start;
using System.Web.Http;

namespace AssetFlow.Legacy.Web
{
    protected void Application_Start()
    {
        // Classic .NET Framework startup: every concern initialized one by one,
        // each in its own App_Start partial. Compare with modern Program.cs.
        ContainerConfig.Register();                 // DI (Autofac)
        GlobalConfiguration.Configure(WebApiConfig.Register);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
}