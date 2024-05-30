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
//        public virtual DbSet<Contact> Contacts { get; set; } = null!;
//        public virtual DbSet<Member> Members { get; set; } = null!;
//        public virtual DbSet<MenuGroup> MenuGroups { get; set; } = null!;
//        public virtual DbSet<MenuSub> MenuSubs { get; set; } = null!;
//        public virtual DbSet<Product> Products { get; set; } = null!;
//        public virtual DbSet<ProductClass> ProductClasses { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=192.168.0.210,61757;Database=RestaurantManagementDB;User ID=sa;Password=Alex0310;Trusted_Connection=True;Integrated Security=False;Encrypt=False;");
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
//                entity.HasKey(e => e.RoleId)
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

//            modelBuilder.Entity<Contact>(entity =>
//            {
//                entity.ToTable("Contact");

//                entity.Property(e => e.ContactReTxt).HasComment("");

//                entity.Property(e => e.CreateTime)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

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

//            modelBuilder.Entity<Product>(entity =>
//            {
//                entity.Property(e => e.Description).HasMaxLength(255);

//                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

//                entity.Property(e => e.ProductName).HasMaxLength(100);

//                entity.HasOne(d => d.Class)
//                    .WithMany(p => p.Products)
//                    .HasForeignKey(d => d.ClassId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__Products__ClassI__5EBF139D");
//            });

//            modelBuilder.Entity<ProductClass>(entity =>
//            {
//                entity.HasKey(e => e.ClassId)
//                    .HasName("PK__ProductC__CB1927C0848D030F");

//                entity.ToTable("ProductClass");

//                entity.Property(e => e.ClassName).HasMaxLength(100);

//                entity.Property(e => e.Description).HasMaxLength(255);
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
}
