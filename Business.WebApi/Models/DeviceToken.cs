using Intersoft.Crosslight;

namespace Business.WebApi.Models
{
    /// <summary>
    ///     Represents device token model.
    /// </summary>
    public class DeviceToken
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the kind of the operating system.
        /// </summary>
        /// <value>
        ///     The kind of the operating system.
        /// </value>
        public OSKind OperatingSystem { get; set; }

        /// <summary>
        ///     Gets or sets the device token.
        /// </summary>
        /// <value>
        ///     The device token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        #endregion
    }
}