using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AssetFlow.Core.Repositories;
using AssetFlow.Core.Services;
using AssetFlow.Legacy.Web.Data;

namespace AssetFlow.Legacy.Web.App_Start;

public static class ContainerConfig
{
    public static void Register()
    {
        var builder = new ContainerBuilder();

        var connStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        builder.Register(_ => new SqlAssetRepository(connStr)).As<IAssetRepository>().InstancePerDependency();
        builder.Register(_ => new SqlUserRepository(connStr)).As<IUserRepository>().InstancePerDependency();
        builder.Register(_ => new SqlAssignmentRepository(connStr)).As<IAssignmentRepository>().InstancePerDependency();

        builder.RegisterType<AssetService>().As<IAssetService>().InstancePerDependency();

        builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        var container = builder.Build();
        GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        // For .aspx code-behind resolution
        WebFormsContainer.Instance = container;
    }
}

internal static class WebFormsContainer
{
    public static IContainer Instance { get; set; }
    public static T Resolve<T>() => Instance.Resolve<T>();
}