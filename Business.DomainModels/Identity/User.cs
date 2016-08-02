using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Business.DomainModel
{
    /// <summary>
    ///     Represents the user model.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityUser" />
    [Table("User")]
    public partial class User : IdentityUser
    {
        #region Properties

        public string Email { get; set; }

        public virtual UserDetail UserDetail { get; set; }

        #endregion
    }
}