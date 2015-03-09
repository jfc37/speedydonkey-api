using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ActionHandlers;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Data;
using Data.Mappings;
using Data.Repositories;
using Data.Searches;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SpeedyDonkeyApi.Services;
using Validation;
using Validation.Validators;
using NHibernate;

namespace SpeedyDonkeyApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other dependencies.
            builder.RegisterAssemblyTypes(typeof(ValidatorOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionValidator<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionHandler<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly)
                .AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(EntitySearch<>))
                .As(typeof(IEntitySearch<>))
                .InstancePerDependency();

            builder.RegisterType<SpeedyDonkeyDbContext>().As<ISpeedyDonkeyDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<ActionHandlerOverlord>().As<IActionHandlerOverlord>();
            builder.RegisterType<ValidatorOverlord>().As<IValidatorOverlord>();
            builder.RegisterType<Container>().As<IContainer>();
            builder.RegisterType<UrlConstructor>().As<IUrlConstructor>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<SearchQueryParser>().As<ISearchQueryParser>();
            builder.RegisterType<QueryFilterModifier>().As<IQueryModifier>();
            builder.RegisterType<ConditionExpressionHandlerFactory>().As<IConditionExpressionHandlerFactory>();
            builder.RegisterType<QueryModifierFactory>().As<IQueryModifierFactory>();

           //new SessionSetup().BuildSchema();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //Setup log4net
            XmlConfigurator.Configure();
        }
    }

    public class SessionSetup
    {
        private readonly IPersistenceConfigurer _persistenceConfigurer;
        private SchemaExport _schemaExport;


        public ISessionFactory GetSessionFactory()
        {
            return Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("SpeedyDonkeyDbContext")))
                            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                            .BuildSessionFactory();
        }

        public void BuildSchema()
        {
            Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(
                        c => c.FromConnectionStringWithKey("SpeedyDonkeyDbContext")))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(false, true, false));
        }
    }
}
