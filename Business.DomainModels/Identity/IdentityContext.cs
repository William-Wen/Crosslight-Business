using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Business.DomainModel
{
    /// <summary>
    ///     Provides the context for user identity.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext{Business.DomainModel.User}" />
    public partial class IdentityContext : IdentityDbContext<User>
    {
        #region Constructors

        public IdentityContext()
            : base("IdentityEntities")
        {
            Database.SetInitializer<IdentityContext>(null);
        }

        #endregion

        #region Properties

        public DbSet<IdentityUserClaim> UserClaims { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<IdentityUserLogin> UserLogins { get; set; }
        public DbSet<IdentityUserRole> UserRoles { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasRequired(o => o.UserDetail).WithRequiredPrincipal();
            modelBuilder.Entity<IdentityUser>().ToTable("Users", "dbo").Property(o => o.Id).HasColumnName("UserId");
            modelBuilder.Entity<User>().ToTable("Users", "dbo").Property(o => o.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo").Property(o => o.Id).HasColumnName("RoleId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo").Property(o => o.Id).HasColumnName("UserClaimId");

            //modelBuilder.Entity<User>().HasRequired(o => o.Claims).WithMany().Map(m => m.MapKey("UserId"));
        }

        #endregion
    }
}