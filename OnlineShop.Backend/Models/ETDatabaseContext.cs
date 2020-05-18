using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineShop.Backend.Models;

namespace OnlineShop.Backend.Models
{ 
    public partial class ETDatabaseContext : DbContext
    {
        public ETDatabaseContext()
        {
        }

        public ETDatabaseContext(DbContextOptions<ETDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ZALNET-PC\\SQLDEV2019;Database=ETDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Klasa Cards
            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("PK_tCards_CardID");

                entity.ToTable("tCards");

                entity.HasComment("Contains info about cards.");

                entity.Property(e => e.CardId).HasColumnName("CardID");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Cvv).HasColumnName("CVV");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.NameOnCard)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            #endregion

            #region Klasa Categories
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_tCategories_CategoryID");

                entity.ToTable("tCategories");

                entity.HasComment("Contains info about categories of products.");

                entity.HasIndex(e => e.CategoryName)
                    .HasName("AK_tCategories_CategorytName")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryDescription).HasColumnType("text");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(25);
            });
            #endregion

            #region Klasa OrderDetails
            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId)
                    .HasName("PK_tOrderDetails_OrderDetailsID");

                entity.ToTable("tOrderDetails");

                entity.HasComment("Contains info about single order.");

                entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PromoId).HasColumnName("PromoID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tOrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tOrderDetails_Products");

                entity.HasOne(d => d.Promo)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.PromoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tOrderDetails_Promotions");
            });
            #endregion

            #region Klasa Orders           
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_tOrders_OrderID");

                entity.ToTable("tOrders");

                entity.HasComment("Contains info about all orders.");

                entity.Property(e => e.Comments).HasColumnType("text");

                entity.Property(e => e.OrderDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.ShippingAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ShippingMethod)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tOrders_Users");
            });
            #endregion

            #region Klasa Products
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_tProducts_ProductID");

                entity.ToTable("tProducts");

                entity.HasComment("Contains info about products in shop.");

                entity.HasIndex(e => e.ProductName)
                    .HasName("AK_tProducts_ProductName")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription).HasColumnType("text");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tProducts_tCategories");
            });
            #endregion

            #region Klasa Promotions
            modelBuilder.Entity<Promotions>(entity =>
            {
                entity.HasKey(e => e.PromoId)
                    .HasName("PK_tPromotions_PromoID");

                entity.ToTable("tPromotions");

                entity.HasComment("Contains information about current promotions.");

                entity.Property(e => e.PromoId).HasColumnName("PromoID");
            });
            #endregion

            #region Klasa Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_tUsers_UserID");

                entity.ToTable("tUsers");

                entity.HasComment("Contains info about single user.");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.PhoneNumber).HasMaxLength(11);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        #endregion
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
