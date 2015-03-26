using System.Configuration;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ActionHandlers;
using ActionHandlers.CreateHandlers.Strategies;
using ActionHandlers.EnrolmentProcess;
using ActionHandlers.UserPasses;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Common;
using Data.Mappings;
using Data.Repositories;
using Data.Searches;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
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

            var dependencyBuilder = new NHibernateDependancySetup();


            // Create the container builder.
            var builder = new ContainerBuilder();
            string connectionString = ConfigurationManager.ConnectionStrings["SpeedyDonkeyDbContext"].ConnectionString;
            dependencyBuilder.AddDependencies(connectionString, builder);

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other dependencies.
            builder.RegisterAssemblyTypes(typeof(ValidatorOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionValidator<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionHandler<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(UserScheduleRepository).Assembly)
                .AsClosedTypesOf(typeof(IAdvancedRepository<,>)).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(EntitySearch<>))
                .As(typeof(IEntitySearch<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterType<ActionHandlerOverlord>().As<IActionHandlerOverlord>();
            builder.RegisterType<ValidatorOverlord>().As<IValidatorOverlord>();
            builder.RegisterType<Container>().As<IContainer>();
            builder.RegisterType<UrlConstructor>().As<IUrlConstructor>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<SearchQueryParser>().As<ISearchQueryParser>();
            builder.RegisterType<QueryFilterModifier>().As<IQueryModifier>();
            builder.RegisterType<ConditionExpressionHandlerFactory>().As<IConditionExpressionHandlerFactory>();
            builder.RegisterType<QueryModifierFactory>().As<IQueryModifierFactory>();
            builder.RegisterType<BlockPopulatorStrategyFactory>().As<IBlockPopulatorStrategyFactory>();
            builder.RegisterType<CommonInterfaceCloner>().As<ICommonInterfaceCloner>();
            builder.RegisterType<PassCreatorFactory>().As<IPassCreatorFactory>();
            builder.RegisterType<UserPassAppender>().As<IUserPassAppender>();
            builder.RegisterType<BlockEnrolmentService>().As<IBlockEnrolmentService>();

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

    public class NHibernateDependancySetup
    {
        public void AddDependencies(string connectionString, ContainerBuilder builder)
        {
            Contract.Requires(connectionString != null); 
            SessionSetup sessionSetup = new SessionSetup(connectionString);
            var sessionFactory = sessionSetup.GetSessionFactory();

            
            builder.RegisterInstance(sessionFactory);

            // Either use a session in view model or per instance depending on the context.
            if (HttpContext.Current != null)
            {
                builder.Register(s => s.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register(s => s.Resolve<ISessionFactory>().OpenSession());
            }
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
}
