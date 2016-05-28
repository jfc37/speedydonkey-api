using System.Web.Http;
using Autofac;
using Data.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Owin;

namespace SpeedyDonkeyApi
{
    public class NHibernateDependancySetup
    {
        public void AddDependencies(string connectionString, ContainerBuilder builder)
        {
            SessionSetup sessionSetup = new SessionSetup(connectionString);
            var sessionFactory = sessionSetup.GetSessionFactory();

            builder.RegisterInstance(sessionFactory);
            builder.Register(s => s.Resolve<ISessionFactory>().OpenSession())
                .InstancePerRequest();
        }
    }

    public class SessionSetup
    {
        private readonly FluentConfiguration _fluentConfiguration;
        public SessionSetup(string connectionString)
        {
            IPersistenceConfigurer persistenceConfigurer = MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionString)
                .AdoNetBatchSize(10);

            _fluentConfiguration = Fluently.Configure()
                .Database(persistenceConfigurer)
                .Diagnostics(x => x.Disable())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>());
        }

        public ISessionFactory GetSessionFactory()
        {
            return _fluentConfiguration.BuildSessionFactory();
        }

        public void BuildSchema()
        {
            var schemaExport = new SchemaExport(_fluentConfiguration.BuildConfiguration());
            schemaExport.Execute(true, true, false);
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IAppBuilder app)
        {
            RouteConfig.Register(config);
            SerailisationConfig.Register(config);
            HttpsConfig.Register(config);
            CorsConfig.Register(config);
            DependancyInjectionConfig.Register(config, app);
            FilterConfig.Register(config);
            FormattersConfig.Register(config);

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
