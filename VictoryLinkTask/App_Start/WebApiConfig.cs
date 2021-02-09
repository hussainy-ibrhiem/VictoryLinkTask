using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using VictoryLinkTask.Repositories.Implementations;
using VictoryLinkTask.Repositories.Interfaces;
using VictoryLinkTask.ServiceLayer.AppService;
using VictoryLinkTask.ServiceLayer.IAppService;

namespace VictoryLinkTask
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IPromotionAppService, PromotionAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
