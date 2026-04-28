using System.Web.Routing;

namespace AssetFlow.Legacy.Web.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Friendly URL: /assets/{id}  ->  ~/Assets/Detail.aspx
            // The {id} segment is exposed via Page.RouteData.Values["id"].
            routes.MapPageRoute(
                routeName: "AssetDetail",
                routeUrl: "assets/{id}",
                physicalFile: "~/Assets/Detail.aspx");
        }
    }
}