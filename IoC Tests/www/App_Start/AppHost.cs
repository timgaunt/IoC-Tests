namespace www.App_Start
{
    using System.Web.Mvc;

    using ServiceStack.Configuration;

    using Funq;

    using ServiceStack.CacheAccess;
    using ServiceStack.CacheAccess.Providers;
    using ServiceStack.Mvc;
    using ServiceStack.ServiceInterface;
    using ServiceStack.WebHost.Endpoints;


    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("IoCTests", typeof(AppStartUp).Assembly)
        {
        }

        public override void Configure(Funq.Container container)
        {
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            registerAdapter(container);

            SetConfig(new EndpointHostConfig { DebugMode = true });

            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>()));

            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
        }

        private static void registerAdapter(Container container)
        {
            var afcontainer = new DependencyBootstrapper().Start();
            IContainerAdapter adapter = new AutofacIocAdapter(afcontainer);
            container.Adapter = adapter;
        }
    }
}