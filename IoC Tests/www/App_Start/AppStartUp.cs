using System.Web;

using www.App_Start;

[assembly: PreApplicationStartMethod(typeof(AppStartUp), "OnPreApplicationStart")]

namespace www.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Autofac.Integration.Mvc;

    public class AppStartUp
    {
        public static void OnPreApplicationStart()
        {
            var container = new DependencyBootstrapper().Start();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            registerAPI();
        }

        private static void registerAPI()
        {
            var host = new AppHost();
            host.Init();
        }
    }
}