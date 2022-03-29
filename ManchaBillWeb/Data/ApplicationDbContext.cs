using ManchaBillWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManchaBillWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Model>()
                .HasOne(e => e.Size)
                .WithMany(e => e.Models)
                .OnDelete(DeleteBehavior.NoAction);


        }



        public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }

        public sealed override int SaveChanges()
        {
            return this.SaveChanges(acceptAllChangesOnSuccess: true);
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is BaseEntity && (
                      e.State == EntityState.Added
                      || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).Active = true;
                }




            }
            return base.SaveChanges(true);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is BaseEntity && (
                      e.State == EntityState.Added
                      || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).Active = true;
                }




            }
            return await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeType> SizeTypes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillLine> BillLines { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<ReturnLine> ReturnLines { get; set; }
        public DbSet<OutFlow> OutFlows { get; set; }


    }
}
