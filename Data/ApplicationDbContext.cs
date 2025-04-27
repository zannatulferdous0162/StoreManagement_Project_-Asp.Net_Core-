using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal object Shelves;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }     
        public DbSet<UnitSet> UnitSets { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<PackSize> PackSizes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<GRN> GRNs { get; set; }
        public DbSet<GRNItem> GRNItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<LocationComponent> LocationComponents { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Aisle> Aisles { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Shelf> shelves { get; set; }
        public DbSet<Bin> Bins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique index on PONumber
            modelBuilder.Entity<PurchaseOrder>()
                .HasIndex(p => p.PONo)
                .IsUnique();
            modelBuilder.Entity<GRN>()
                .HasIndex(g => g.GRNNumber)
                .IsUnique();
        }
    }
}
