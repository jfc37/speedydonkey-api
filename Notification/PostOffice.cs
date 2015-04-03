using System;
using Autofac;
using Autofac.Core.Registration;
using Notification.NotificationHandlers;
using Notification.Notifications;

namespace Notification
{
    public interface IPostOffice
    {
        void Send<T>(T notification) where T : INotification;
    }
    public class PostOffice : IPostOffice
    {
        private readonly ILifetimeScope _container;

        public PostOffice(ILifetimeScope container)
        {
            _container = container;
        }

        public void Send<T>(T notification) where T : INotification
        {
            var notificationHandler = GetNotificationHandler<T>();
            notificationHandler.Handle(notification);
        }

        private INotificationHandler<T> GetNotificationHandler<T>() where T : INotification
        {
            try
            {
                var handler = _container.Resolve<INotificationHandler<T>>();
                return handler;
            }
            catch (ComponentNotRegisteredException exception)
            {
                throw new ArgumentException(String.Format("There isn't a notification handler for notification type {0}", typeof(T)), exception);
            }
        }
    }
}
