using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        // bu options db yolunu startup dosyasından vermemi sağlayacak
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //dbSet veritabanındaki tabloları ifade ediyor. 

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Bu assembly IEntityConfiguration interface'ine sahip olan bütün classları getiriyor. 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityRef)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityRef.CreatedDate = DateTime.Now;
                                break;

                            }
                        case EntityState.Modified:
                            {
                                Entry(entityRef).Property(x=>x.CreatedDate).IsModified = false;
                                entityRef.UpdateDate = DateTime.Now;
                                break;
                            }
                    }

                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach( var item in ChangeTracker.Entries()) 
            {
               if(item.Entity is BaseEntity entityRef )
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityRef.CreatedDate = DateTime.Now;
                                break;

                            }
                            case EntityState.Modified:
                            {
                                Entry(entityRef).Property(x=>x.CreatedDate).IsModified= false;
                                entityRef.UpdateDate = DateTime.Now;
                                break;
                            }
                    }

                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
