using System.Web.Routing;

namespace AssetFlow.Legacy.Web.App_Start;

public static class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        // Friendly URLs are handled by ASP.NET Web Forms routing extensions
    }
}