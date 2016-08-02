using System;
using System.Collections.Generic;

namespace Business.DomainModel
{
    /// <summary>
    ///     Represents the values and validation rules for user registration.
    /// </summary>
    public partial class RegistrationData
    {
        #region Properties

        public DateTime? BirthDate { get; set; }

        /// <summary>
        ///     Gets and sets the email address.
        /// </summary>
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public Dictionary<string, string> UserClaims { get; set; }
        public Dictionary<string, string> UserLogins { get; set; }

        /// <summary>
        ///     Gets and sets the user name.
        /// </summary>
        public string UserName { get; set; }

        public List<string> UserRoles { get; set; }

        #endregion
    }
}