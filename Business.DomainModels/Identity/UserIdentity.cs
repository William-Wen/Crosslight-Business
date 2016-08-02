using System.Security.Principal;

namespace Business.DomainModel
{
    /// <summary>
    ///     Represents the user identity model.
    /// </summary>
    /// <seealso cref="System.Security.Principal.GenericIdentity" />
    public class UserIdentity : GenericIdentity
    {
        #region Constructors

        public UserIdentity(string name)
            : base(name)
        {
        }

        public UserIdentity(string name, string type)
            : base(name, type)
        {
        }

        #endregion

        #region Properties

        public string Email { get; set; }

        #endregion
    }
}