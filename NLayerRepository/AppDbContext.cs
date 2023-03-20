using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        // bu options db yolunu startup dosyasından vermemi sağlayacak
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        //dbSet veritabanındaki tabloları ifade ediyor. 

        public DbSet<Category>  Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Bu assembly IEntityConfiguration interface'ine sahip olan bütün classları getiriyor. 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           
            base.OnModelCreating(modelBuilder);
        }
    }
}
