using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DomainModel
{
    /// <summary>
    ///     Represents the user detail model.
    /// </summary>
    public partial class UserDetail
    {
        #region Properties

        public Nullable<DateTime> BirthDate { get; set; }
        public string FirstName { get; set; }
        public string ImageUrl { get; set; }
        public string LastName { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key]
        public string UserId { get; set; }

        #endregion
    }
}