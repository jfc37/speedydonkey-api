using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using ActionHandlers;
using AuthZero.Domain.Clients;
using AuthZero.Domain.EmailVerification;
using AuthZero.Interfaces;
using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Common;
using Core.Queries.Reports.TeacherInvoices;
using Data.Repositories;
using Data.Searches;
using Notification;
using Notification.NotificationHandlers;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using Owin;
using Queries.Reports.TeacherInvoices;
using SpeedyDonkeyApi.Extensions.Models;
using Validation;
using Validation.Validators;

namespace SpeedyDonkeyApi
{
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
                typeof (PostOffice).Assembly,
                typeof (AuthZeroClientRepository).Assembly,
                typeof (IAuthZeroClientRepository).Assembly,
                typeof (ITeacherInvoiceReportGenerator).Assembly,
                typeof (TeacherInvoiceReportGenerator).Assembly
            };
            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces();

            builder.RegisterType<Container>().As<IContainer>();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().InstancePerLifetimeScope();

            var actualAuth0Integration = Convert.ToBoolean(new AppSettings().GetSetting(AppSettingKey.AuthZeroRealIntegration));
            if (actualAuth0Integration)
            {
                builder.RegisterType<AuthZeroClientRepository>().As<IAuthZeroClientRepository>();
                builder.RegisterType<AuthZeroEmailVerificationRepository>().As<IAuthZeroEmailVerificationRepository>();
            }
            else
            {
                builder.RegisterType<FakeAuthZeroClientRepository>().As<IAuthZeroClientRepository>();
                builder.RegisterType<FakeAuthZeroEmailVerificationRepository>().As<IAuthZeroEmailVerificationRepository>();
            }

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
}