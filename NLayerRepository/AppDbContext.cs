using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        #region Constructor
        // bu options db yolunu startup dosyasından vermemi sağlayacak
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        #endregion

        #region Property
        //dbSet veritabanındaki tabloları ifade ediyor. 

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Bu assembly IEntityConfiguration interface'ine sahip olan bütün classları getiriyor.
            //TODO:Seed data ve congifurationsları efcore'a bildirebilmek için bunu kullanmak mı zorundayım?
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            //ChangeTracker: DbContext instance'larıyla çalışan her varlığın durumunu izler. modelState= Added,Modified vs. 
            #region Date Setter
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityRef)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entityRef).Property(x => x.UpdateDate).IsModified = false;
                                entityRef.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityRef).Property(x => x.CreatedDate).IsModified = false;
                                entityRef.UpdateDate = DateTime.Now;
                                break;
                            }
                    }
                }
            } 
            #endregion

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            #region Date Setter
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entryRef)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entryRef).Property(x => x.UpdateDate).IsModified = false;
                                entryRef.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entryRef).Property(x => x.CreatedDate).IsModified = false;
                                entryRef.UpdateDate = DateTime.Now;
                                break;
                            }
                    }
                }
            } 
            #endregion
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
