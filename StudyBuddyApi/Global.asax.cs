using System.Configuration;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ActionHandlers;
using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Common;
using Data;
using Data.Mappings;
using Data.Repositories;
using Data.Searches;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using Mindscape.Raygun4Net.WebApi;
using NHibernate.Tool.hbm2ddl;
using Notification;
using Notification.NotificationHandlers;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Filter;
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
            GlobalConfiguration.Configuration.Filters.Add(new NullModelActionFilter());
            GlobalConfiguration.Configuration.Filters.Add(new ValidateModelActionFilter());
            GlobalConfiguration.Configuration.Filters.Add(new CurrentUserActionFilter());

            var dependencyBuilder = new NHibernateDependancySetup();





            // Create the container builder.
            var builder = new ContainerBuilder();
            string connectionString = ConfigurationManager.ConnectionStrings["SpeedyDonkeyDbContext"].ConnectionString;
            dependencyBuilder.AddDependencies(connectionString, builder);

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            // Register other dependencies.
            builder.RegisterAssemblyTypes(typeof(ValidatorOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionValidator<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionHandlerWithResult<,,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionHandler<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(UserScheduleRepository).Assembly)
                .AsClosedTypesOf(typeof(IAdvancedRepository<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(PayPalPaymentStrategy).Assembly)
                .AsClosedTypesOf(typeof(IStartPaymentStrategy<,>)).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(EntitySearch<>))
                .As(typeof(IEntitySearch<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(NotificationHandler<>))
                .As(typeof(INotificationHandler<>))
                .InstancePerDependency();

            var assemblies = new[]
            {
                typeof (ExpressCheckout).Assembly,
                typeof (ActionHandlerOverlord).Assembly,
                typeof (ValidatorOverlord).Assembly,
                typeof (OnlinePaymentRequestModelExtensions).Assembly,
                typeof (SearchQueryParser).Assembly,
                typeof (CommonInterfaceCloner).Assembly,
                typeof (PostOffice).Assembly,

            };
            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces();

            builder.RegisterType<Container>().As<IContainer>();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().InstancePerLifetimeScope();
            builder.RegisterType<ActivityLogger>().As<IActivityLogger>().InstancePerLifetimeScope();

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
}
