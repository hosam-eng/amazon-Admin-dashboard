using AmazonAdmin.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<shippingAddress> ShippingAddresses{ get; set; }
        public virtual DbSet<Rating> Ratings{ get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(p => new { p.Phone, p.EmailAddress })
            .IsUnique()
            .HasFilter("Phone IS NOT NULL AND EmailAddress IS NOT NULL");

            modelBuilder.Entity<Product>(p =>
            {
                p.Property(s => s.Status).HasDefaultValue(true);
            });
            base.OnModelCreating(modelBuilder);

        }
    }
}
