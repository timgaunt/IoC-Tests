namespace www.App_Start
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;

    using Autofac;
    using Autofac.Integration.Mvc;

    using IoCTests.Common.Database;
    using IoCTests.Common.Entities;
    using IoCTests.Common.Interfaces;
    using IoCTests.Common.Repositories;
    using IoCTests.Common.ServiceImplementations;

    public class DependencyBootstrapper
    {
        private ContainerBuilder _builder;

        public IContainer Start()
        {
            _builder = new ContainerBuilder();
            _builder.RegisterFilterProvider();
            RegisterControllers();
            return _builder.Build();
        }

        private void RegisterControllers()
        {
            RegisterAssembly(Assembly.GetExecutingAssembly());
            _builder.RegisterModelBinderProvider();

            RegisterPerLifetimeConnections();
            RegisterRepositories();
            RegisterServices();
        }

        private void RegisterAssembly(Assembly assembly)
        {
            _builder.RegisterModelBinders(assembly);
            _builder.RegisterControllers(assembly);
        }

        private void RegisterRepositories()
        {
            _builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));
            _builder.RegisterType<GenericRepository<SomeObject>>().As<IRepository<SomeObject>>();
        }

        private void RegisterServices()
        {
            _builder.RegisterType<SomeObjectService>().As<ISomeObjectService>();
        }

        private void RegisterPerLifetimeConnections()
        {
            const string connectionStringName = "IoCTests";
            _builder.RegisterType<IoCTestsContext>()
                .As<DbContext>()
                .WithParameter("connectionStringName", connectionStringName)
                .InstancePerLifetimeScope();

            _builder.Register(c => new HttpContextWrapper(HttpContext.Current))
                .As<HttpContextBase>();
        }
    }
}