









//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Business.DomainModels.Inventory
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class InventoryEntities : DbContext
    {
        public InventoryEntities()
            : base("name=InventoryEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        private static bool _isInitialized;

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                Business.DomainModels.Inventory.Category._Initialize();
                Business.DomainModels.Inventory.Item._Initialize();
            }
        }
    }
}
