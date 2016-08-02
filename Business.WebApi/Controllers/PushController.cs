using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.DomainModels.PushNotification;
using Business.WebApi.Models;
using Intersoft.Messaging.PushService;

namespace Business.WebApi.Controllers
{
    public class PushController : Controller
    {
        #region Fields

        private PushNotificationEntities _context;

        #endregion

        #region Properties

        public PushNotificationEntities Context
        {
            get
            {
                if (this._context == null)
                    this._context = new PushNotificationEntities();

                return this._context;
            }
        }

        #endregion

        #region Methods

        public ActionResult SendNotification()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult SendNotification(PushNotification notification)
        {
            PushServiceManager manager = PushServiceManager.Instance;
            IEnumerable<PushToken> tokens = null;

            if (!string.IsNullOrEmpty(notification.UserName))
            {
                IdentityController controller = new IdentityController();
                DomainModel.User user = controller.GetUserProfile(notification.UserName);

                if (user != null)
                {
                    tokens = this.Context.DeviceTokens.ToList().Where(o => o.UserId == user.Id).Select(o => new PushToken
                    {
                        DeviceToken = o.Token,
                        OperatingSystem = PushServiceManager.GetOperatingSystem(o.OperatingSystem)
                    });
                }
            }
            else
            {
                tokens = this.Context.DeviceTokens.ToList().Select(o => new PushToken
                {
                    DeviceToken = o.Token,
                    OperatingSystem = PushServiceManager.GetOperatingSystem(o.OperatingSystem)
                });

                Dictionary<string, string> extraData = new Dictionary<string, string>();
                extraData.Add("Key1", "Value1");
                extraData.Add("Key2", "Value2");
            }

            // standard way to queue notifications to all supported platforms
            manager.QueueNotification(tokens, notification.Title, notification.Message);

            // The following example demonstrates unified notification (UNotification) class.
            // It lets you easily send notifications to all platforms while allowing you to
            // customize the notification behaviors and payload for specific platform.

            // var uNotification = new UNotification(notification.Title, notification.Message);
            // uNotification.AppleNotification.Payload.Badge = 2;
            // uNotification.AppleNotification.Payload.Sound = "default";
            // manager.QueueNotification(tokens, uNotification);

            return View();
        }

        #endregion
    }
}