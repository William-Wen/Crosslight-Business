using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.DomainModels.PushNotification;
using Intersoft.Messaging.PushService;

namespace Business.WebApi.Controllers
{
    public class PushNotificationController : ApiController
    {
        #region Fields

        private PushNotificationEntities _context;

        #endregion

        #region Properties

        public PushNotificationEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new PushNotificationEntities();

                return _context;
            }
        }

        #endregion

        #region Methods

        [HttpPost]
        public virtual HttpResponseMessage RegisterDevice(Models.DeviceToken deviceTokenModel)
        {
            string operatingSystem = deviceTokenModel.OperatingSystem.ToString();

            DeviceToken deviceToken = this.Context.DeviceTokens.FirstOrDefault(o => o.OperatingSystem == operatingSystem && o.Token == deviceTokenModel.Token);

            if (deviceToken == null)
            {
                deviceToken = new DeviceToken
                {
                    DeviceTokenId = Guid.NewGuid().ToString(),
                    OperatingSystem = operatingSystem,
                    Token = deviceTokenModel.Token
                };

                if (!string.IsNullOrEmpty(deviceTokenModel.UserId))
                    deviceToken.UserId = deviceTokenModel.UserId;

                this.Context.DeviceTokens.Add(deviceToken);
                this.Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Device token already exist.");
        }

        [HttpGet]
        public virtual HttpResponseMessage SendNotification(string title, string message, string userId = "")
        {
            PushServiceManager manager = PushServiceManager.Instance;
            IEnumerable<PushToken> tokens = null;

            if (string.IsNullOrEmpty(userId))
            {
                tokens = this.Context.DeviceTokens.ToList().Select(o => new PushToken
                {
                    DeviceToken = o.Token,
                    OperatingSystem = PushServiceManager.GetOperatingSystem(o.OperatingSystem)
                });
            }
            else
            {
                tokens = this.Context.DeviceTokens.Where(o => o.UserId == userId).ToList().Select(o => new PushToken
                {
                    DeviceToken = o.Token,
                    OperatingSystem = PushServiceManager.GetOperatingSystem(o.OperatingSystem)
                });
            }

            manager.QueueNotification(tokens, title, message);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual HttpResponseMessage UpdateDeviceToken(Models.DeviceToken deviceToken)
        {
            string operatingSystem = deviceToken.OperatingSystem.ToString();
            string token = deviceToken.Token;

            DeviceToken currentToken = this.Context.DeviceTokens.FirstOrDefault(o => o.OperatingSystem == operatingSystem && o.Token == token);

            if (currentToken != null)
            {
                currentToken.UserId = deviceToken.UserId;
                this.Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateResponse(HttpStatusCode.Conflict, "Device token not exist.");
        }

        #endregion
    }
}