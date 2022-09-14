using UnityEngine;
using Unity.Notifications.Android;

public class MobileNotifications : MonoBehaviour
{
    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Teste de titulo";
        notification.Text = "Ola, isso e um teste";
        notification.FireTime = System.DateTime.Now.AddHours(30);

        int id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
