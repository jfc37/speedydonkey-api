using System;
using ActionHandlers;
using Autofac;
using Notification;
using Notification.NotificationHandlers;
using Validation;
using Validation.Validators;

namespace Common.Tests.Builders
{
    public class LifetimeScopeBuilder
    {
        private ContainerBuilder _containerBuilder;

        public LifetimeScopeBuilder()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public ILifetimeScope Build()
        {
            return _containerBuilder.Build();
        }

        public LifetimeScopeBuilder WithActionValidatorsRegistered()
        {
            _containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IActionValidator<,>)).AsImplementedInterfaces();
            return this;
        }

        public LifetimeScopeBuilder WithNothingRegistered()
        {
            _containerBuilder = new ContainerBuilder();
            return this;
        }

        public LifetimeScopeBuilder WithActionHandlersRegistered()
        {
            _containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IActionHandler<,>)).AsImplementedInterfaces();
            return this;
        }

        public LifetimeScopeBuilder WithNotificationHandlersRegistered()
        {
            _containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(INotificationHandler<>)).AsImplementedInterfaces();
            return this;
        }
    }
}
