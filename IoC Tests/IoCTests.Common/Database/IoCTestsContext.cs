namespace IoCTests.Common.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Reflection;

    using IoCTests.Common.Entities;
    using IoCTests.Common.Interfaces;
    using IoCTests.Common.ModelBuilders;

    public class IoCTestsContext : DbContext, IIoCTestsContext
    {
        public IDbSet<SomeObject> SomeObjects { get; set; }

        static IoCTestsContext()
        {
            Database.SetInitializer(new IoCTestsDatabaseInitilizer());
        }

        public IoCTestsContext(string connectionStringName)
            : base(connectionStringName)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public IoCTestsContext()
            : this("name=IoCTests")
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BuildModels(modelBuilder);
        }

        private void BuildModels(DbModelBuilder builder)
        {
            var typeToUse = typeof(SomeObjectModelBuilder);
            var namespaceToUse = typeToUse.Namespace;

            var toReg =
                Assembly.GetAssembly(typeToUse)
                        .GetTypes()
                        .Where(type => type.Namespace != null && type.Namespace.StartsWith(namespaceToUse))
                        .Where(type => type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (object configurationInstance in toReg.Select(Activator.CreateInstance))
            {
                builder.Configurations.Add((dynamic)configurationInstance);
            }
        }
    }
}