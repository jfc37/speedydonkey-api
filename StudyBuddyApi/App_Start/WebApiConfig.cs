using System.Configuration;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using ActionHandlers;
using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Common;
using Data.Mappings;
using Data.Repositories;
using Data.Searches;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
#if !DEBUG
using SpeedyDonkeyApi.Filter;
#endif
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Notification;
using Notification.NotificationHandlers;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using Owin;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Filter;
using Validation;
using Validation.Validators;

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
    public static class DependancyInjectionConfig
    {
        public static void Register(HttpConfiguration config, IAppBuilder app)
        {
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
                typeof (PostOffice).Assembly
            };
            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces();

            builder.RegisterType<Container>().As<IContainer>();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().InstancePerLifetimeScope();


            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            config.DependencyResolver = resolver;

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }
    }

    public static class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
    public static class HttpsConfig
    {
        public static void Register(HttpConfiguration config)
        {
#if !DEBUG
            //Force HTTPS on entire API
            config.Filters.Add(new RequireHttpsAttribute());
#endif
        }
    }
    public static class CorsConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Specify values as appropriate (origins,headers,methods)
            var websiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            websiteUrl = "https://" + websiteUrl;
            if (websiteUrl == "https://spa-speedydonkey.azurewebsites.net")
                websiteUrl = "https://spa-speedydonkey.azurewebsites.net,http://localhost:7300,http://localhost:3000";
            var cors = new EnableCorsAttribute(websiteUrl, "*", "*");
            config.EnableCors(cors);
        }
    }
    public static class SerailisationConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Default to json
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            //Serialise enums to strings
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;

            //Camel case json
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            config.Filters.Add(new NullModelActionFilter());
            config.Filters.Add(new ValidateModelActionFilter());
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
        }
    }
}
