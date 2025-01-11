using Eticaret.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Eticaret.Data
{
    public class DatabaseContext:DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=DESKTOP-A3V78GH\SQLEXPRESS01; 
            Database=EticaretDb;
            Trusted_Connection=True;
            TrustServerCertificate=True;");



            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//çalışan dll in içinden bul
            base.OnModelCreating(modelBuilder);
        }
    }
}
