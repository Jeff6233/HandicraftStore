using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Handicraft.Models
{
    public class HandicraftContext : DbContext
    {
        public HandicraftContext (DbContextOptions<HandicraftContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ShoppingCar> ShoppingCar { get; set; }
        public virtual DbSet<UserCollect> UserCollect { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<BannerImage> BannerImage { get; set; }
        public virtual DbSet<DetailImage> DetailImage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product")
                .HasMany(e => e.ProductImage).WithOne(e => e.Product).HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductImage>().ToTable("ProductImage")
                .HasMany(e=>e.BannerImage).WithOne(e=>e.ProductImage).HasForeignKey(e=>e.ProductImageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductImage>().ToTable("ProductImage")
                .HasMany(e => e.DetailImage).WithOne(e => e.ProductImage).HasForeignKey(e => e.ProductImageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShoppingCar>().ToTable("ShoppingCar")
                .HasMany(e => e.Product);

            modelBuilder.Entity<UserInfo>().ToTable("UserInfo")
                .HasMany(e=>e.ShoppingCar).WithOne(e=>e.UserInfo).HasForeignKey(e=>e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserInfo>().ToTable("UserInfo")
                .HasMany(e => e.UserCollect).WithOne(e => e.UserInfo).HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserCollect>().ToTable("UserCollect");

            modelBuilder.Entity<BannerImage>().ToTable("BannerImage");

            modelBuilder.Entity<DetailImage>().ToTable("DetailImage");
        }
    }
}
