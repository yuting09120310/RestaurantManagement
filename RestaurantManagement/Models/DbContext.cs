using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RestaurantManagement.Models
{
//    public partial class DbContext : DbContext
//    {
//        public DbContext()
//        {
//        }

//        public DbContext(DbContextOptions<DbContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<Admin> Admins { get; set; } = null!;
//        public virtual DbSet<AdminGroup> AdminGroups { get; set; } = null!;
//        public virtual DbSet<AdminRole> AdminRoles { get; set; } = null!;
//        public virtual DbSet<Banner> Banners { get; set; } = null!;
//        public virtual DbSet<Cart> Carts { get; set; } = null!;
//        public virtual DbSet<Contact> Contacts { get; set; } = null!;
//        public virtual DbSet<Member> Members { get; set; } = null!;
//        public virtual DbSet<MenuGroup> MenuGroups { get; set; } = null!;
//        public virtual DbSet<MenuSub> MenuSubs { get; set; } = null!;
//        public virtual DbSet<Order> Orders { get; set; } = null!;
//        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
//        public virtual DbSet<Permission> Permissions { get; set; } = null!;
//        public virtual DbSet<Product> Products { get; set; } = null!;
//        public virtual DbSet<ProductClass> ProductClasses { get; set; } = null!;
//        public virtual DbSet<ProductView> ProductViews { get; set; } = null!;
//        public virtual DbSet<View1> View1s { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=192.168.0.111;Database=RestaurantManagementDB;User ID=sa;Password=Alex0310;Trusted_Connection=True;Integrated Security=False;Encrypt=False;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Admin>(entity =>
//            {
//                entity.ToTable("Admin");

//                entity.Property(e => e.CreateTime)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.EditTime).HasColumnType("datetime");

//                entity.Property(e => e.Ip).HasColumnName("IP");

//                entity.Property(e => e.LastLogin).HasColumnType("datetime");
//            });

//            modelBuilder.Entity<AdminGroup>(entity =>
//            {
//                entity.HasKey(e => e.GroupId)
//                    .HasName("PK_LoginGroup");

//                entity.ToTable("AdminGroup");

//                entity.Property(e => e.CreateTime)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.EditTime).HasColumnType("datetime");

//                entity.Property(e => e.Ip).HasColumnName("IP");
//            });

//            modelBuilder.Entity<AdminRole>(entity =>
//            {
//                entity.HasKey(e => e.RoleNum)
//                    .HasName("PK_LoginRole");

//                entity.ToTable("AdminRole");

//                entity.Property(e => e.CreateTime)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.Ip).HasColumnName("IP");
//            });

//            modelBuilder.Entity<Banner>(entity =>
//            {
//                entity.ToTable("Banner");

//                entity.Property(e => e.BannerContxt).HasComment("");

//                entity.Property(e => e.BannerOffTime).HasColumnType("datetime");

//                entity.Property(e => e.BannerPutTime).HasColumnType("datetime");

//                entity.Property(e => e.CreateTime).HasColumnType("datetime");

//                entity.Property(e => e.EditTime).HasColumnType("datetime");

//                entity.Property(e => e.Ip).HasColumnName("IP");
//            });

//            modelBuilder.Entity<Cart>(entity =>
//            {
//                entity.ToTable("Cart");

//                entity.Property(e => e.CreatedDate)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");
//            });

//            modelBuilder.Entity<Contact>(entity =>
//            {
//                entity.ToTable("Contact");

//                entity.Property(e => e.ContactReTxt).HasComment("");

//                entity.Property(e => e.Ip).HasColumnName("IP");
//            });

//            modelBuilder.Entity<Member>(entity =>
//            {
//                entity.ToTable("Member");

//                entity.Property(e => e.Ip).HasColumnName("IP");

//                entity.Property(e => e.LastLogin).HasColumnType("datetime");
//            });

//            modelBuilder.Entity<MenuGroup>(entity =>
//            {
//                entity.ToTable("MenuGroup");

//                entity.Property(e => e.CreateTime)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.EditTime).HasColumnType("datetime");

//                entity.Property(e => e.Ip).HasColumnName("IP");
//            });

//            modelBuilder.Entity<MenuSub>(entity =>
//            {
//                entity.ToTable("MenuSub");

//                entity.Property(e => e.CreateTime)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.EditTime).HasColumnType("datetime");

//                entity.Property(e => e.Ip).HasColumnName("IP");
//            });

//            modelBuilder.Entity<Order>(entity =>
//            {
//                entity.ToTable("Order");

//                entity.Property(e => e.OrderDate).HasColumnType("datetime");

//                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
//            });

//            modelBuilder.Entity<OrderDetail>(entity =>
//            {
//                entity.HasKey(e => e.OrderDetailsId);

//                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
//            });

//            modelBuilder.Entity<Permission>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToView("Permissions");
//            });

//            modelBuilder.Entity<Product>(entity =>
//            {
//                entity.ToTable("Product");

//                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
//            });

//            modelBuilder.Entity<ProductClass>(entity =>
//            {
//                entity.ToTable("ProductClass");
//            });

//            modelBuilder.Entity<ProductView>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToView("ProductView");

//                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
//            });

//            modelBuilder.Entity<View1>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToView("View_1");

//                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
}
