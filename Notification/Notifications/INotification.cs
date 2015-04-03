namespace Notification.Notifications
{
    public interface INotification
    {
        string EmailTo { get; }
        string Subject { get; }
        string EmailBody { get; set; }
    }
}