using Android.App;
using Android.Content;
using Intersoft.Crosslight.Services.PushNotification.Android;

namespace Business.Android.Infrastructure
{
    /// <summary>
    ///     Represents application's Google Cloud Messaging Boot Receiver.
    ///     This class reacts after the ActionBootCompleted and ActionUserPresent IntentFilter are triggered.
    /// </summary>
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    [IntentFilter(new[] { Intent.ActionUserPresent })]
    public class NotificationBootReceiver : GoogleCloudMessagingBootReceiver
    {
        #region Properties

        /// <summary>
        /// Gets or sets the notification icon identifier.
        /// </summary>
        /// <value>
        /// The notification icon identifier.
        /// </value>
        public override int NotificationIconId
        {
            get { return Resource.Mipmap.icon; }
        }

        #endregion
    }
}