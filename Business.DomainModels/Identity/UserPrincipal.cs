using System.Security.Principal;

namespace Business.DomainModel
{
    /// <summary>
    ///     Represents the user principal model.
    /// </summary>
    /// <seealso cref="System.Security.Principal.GenericPrincipal" />
    public class UserPrincipal : GenericPrincipal
    {
        #region Constructors

        public UserPrincipal(IIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }

        #endregion
    }
}