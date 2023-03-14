using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LuxuryShop.Data.Models
{
    public partial class dbMarketsContext : DbContext
    {
        public dbMarketsContext()
        {
        }

        public dbMarketsContext(DbContextOptions<dbMarketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AttributesPrice> AttributesPrices { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryTranslation> CategoryTranslations { get; set; } = null!;
        public virtual DbSet<CheckWh> CheckWhs { get; set; } = null!;
        public virtual DbSet<CheckWhdetail> CheckWhdetails { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<ImportBill> ImportBills { get; set; } = null!;
        public virtual DbSet<ImportBillDetail> ImportBillDetails { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<ListProductImage> ListProductImages { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<PriceHistory> PriceHistories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductTranslation> ProductTranslations { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<PromotionsDetail> PromotionsDetails { get; set; } = null!;
        public virtual DbSet<QuangCao> QuangCaos { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<Specification> Specifications { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<TinDang> TinDangs { get; set; } = null!;
        public virtual DbSet<TransactStatus> TransactStatuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WareHouse> WareHouses { get; set; } = null!;
        public virtual DbSet<WareHouseDetail> WareHouseDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=dbMarkets;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Accounts_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Accounts_User");
            });

            modelBuilder.Entity<AttributesPrice>(entity =>
            {
                entity.Property(e => e.AttributesPriceId).HasColumnName("AttributesPriceID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributesPrices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributesPrices_Products");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CatName).HasMaxLength(250);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<CategoryTranslation>(entity =>
            {
                entity.ToTable("CategoryTranslation");

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.LanguageId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SeoAlias).HasMaxLength(200);

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoTitle).HasMaxLength(200);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.CategoryTranslations)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryTranslation_Category");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.CategoryTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryTranslation_Language");
            });

            modelBuilder.Entity<CheckWh>(entity =>
            {
                entity.HasKey(e => e.CheckId);

                entity.ToTable("CheckWH");

                entity.Property(e => e.CheckId).HasColumnName("CheckID");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.EndDay).HasColumnType("datetime");

                entity.Property(e => e.StartDay).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WareHouseId).HasColumnName("WareHouseID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CheckWhs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CheckWH_Users");

                entity.HasOne(d => d.WareHouse)
                    .WithMany(p => p.CheckWhs)
                    .HasForeignKey(d => d.WareHouseId)
                    .HasConstraintName("FK_CheckWH_WareHouse");
            });

            modelBuilder.Entity<CheckWhdetail>(entity =>
            {
                entity.ToTable("CheckWHDetail");

                entity.Property(e => e.CheckWhdetailId).HasColumnName("CheckWHDetailID");

                entity.Property(e => e.CheckWh).HasColumnName("CheckWH");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.CheckWhNavigation)
                    .WithMany(p => p.CheckWhdetails)
                    .HasForeignKey(d => d.CheckWh)
                    .HasConstraintName("FK_CheckWHDetail_CheckWHD");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CheckWhdetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_CheckWHDetail_Product");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(8)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ImportBill>(entity =>
            {
                entity.ToTable("ImportBill");

                entity.Property(e => e.ImportBillId).HasColumnName("ImportBillID");

                entity.Property(e => e.BillNumber)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ImportBills)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_ImportBill_Supplier");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ImportBills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ImportBill_Users");
            });

            modelBuilder.Entity<ImportBillDetail>(entity =>
            {
                entity.HasKey(e => e.Ibdid);

                entity.ToTable("ImportBillDetail");

                entity.Property(e => e.Ibdid).HasColumnName("IBDID");

                entity.Property(e => e.ImportBillId).HasColumnName("ImportBillID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.ImportBill)
                    .WithMany(p => p.ImportBillDetails)
                    .HasForeignKey(d => d.ImportBillId)
                    .HasConstraintName("FK_ImportBillDetail_ImportBill");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImportBillDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ImportBillDetail_Product");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ListProductImage>(entity =>
            {
                entity.ToTable("ListProductImage");

                entity.Property(e => e.ListProductImageId).HasColumnName("ListProductImageID");

                entity.Property(e => e.Caption).HasMaxLength(200);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ListProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ListProductImage_Products");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameWithType).HasMaxLength(100);

                entity.Property(e => e.Slug).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(10);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_TransactStatus");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PageName).HasMaxLength(250);

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<PriceHistory>(entity =>
            {
                entity.HasKey(e => e.PriceId);

                entity.ToTable("PriceHistory");

                entity.Property(e => e.PriceId).HasColumnName("PriceID");

                entity.Property(e => e.EndDay).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StartDay).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PriceHistories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PriceHistory_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ShortDesc).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<ProductTranslation>(entity =>
            {
                entity.ToTable("ProductTranslation");

                entity.Property(e => e.Details).HasMaxLength(500);

                entity.Property(e => e.LanguageId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SeoAlias).HasMaxLength(200);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ProductTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTranslation_Language");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTranslations)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTranslation_Product");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.PromotionsId);

                entity.Property(e => e.PromotionsId).HasColumnName("PromotionsID");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.PromotionsName).HasMaxLength(250);
            });

            modelBuilder.Entity<PromotionsDetail>(entity =>
            {
                entity.ToTable("PromotionsDetail");

                entity.Property(e => e.PromotionsDetailId).HasColumnName("PromotionsDetailID");

                entity.Property(e => e.EndDay).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PromotionsId).HasColumnName("PromotionsID");

                entity.Property(e => e.StartDay).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PromotionsDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PromotionsDetail_Product");

                entity.HasOne(d => d.Promotions)
                    .WithMany(p => p.PromotionsDetails)
                    .HasForeignKey(d => d.PromotionsId)
                    .HasConstraintName("FK_PromotionsDetail_Promotions");
            });

            modelBuilder.Entity<QuangCao>(entity =>
            {
                entity.Property(e => e.QuangCaoId).HasColumnName("QuangCaoID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ImageBg)
                    .HasMaxLength(250)
                    .HasColumnName("ImageBG");

                entity.Property(e => e.ImageProduct).HasMaxLength(250);

                entity.Property(e => e.SubTitle).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UrlLink).HasMaxLength(250);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.SaleId).HasColumnName("SaleID");

                entity.Property(e => e.EndDay).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StartDay).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Sale_Product");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.Company).HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipperName).HasMaxLength(150);
            });

            modelBuilder.Entity<Specification>(entity =>
            {
                entity.Property(e => e.SpecificationId).HasColumnName("SpecificationID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SpecificationName).HasMaxLength(150);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Specifications)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Specifications_Product");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SupplierName).HasMaxLength(250);
            });

            modelBuilder.Entity<TinDang>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK_tblTinTucs");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsHot).HasColumnName("isHot");

                entity.Property(e => e.IsNewfeed).HasColumnName("isNewfeed");

                entity.Property(e => e.MetaDesc).HasMaxLength(255);

                entity.Property(e => e.MetaKey).HasMaxLength(255);

                entity.Property(e => e.Scontents)
                    .HasMaxLength(255)
                    .HasColumnName("SContents");

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<TransactStatus>(entity =>
            {
                entity.ToTable("TransactStatus");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address).HasMaxLength(1500);

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Thumb)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(250);
            });

            modelBuilder.Entity<WareHouse>(entity =>
            {
                entity.ToTable("WareHouse");

                entity.Property(e => e.WareHouseId).HasColumnName("WareHouseID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.WareHouseName).HasMaxLength(250);
            });

            modelBuilder.Entity<WareHouseDetail>(entity =>
            {
                entity.ToTable("WareHouseDetail");

                entity.Property(e => e.WareHouseDetailId).HasColumnName("WareHouseDetailID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.WareHouseId).HasColumnName("WareHouseID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WareHouseDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_WareHouseDetail_Product");

                entity.HasOne(d => d.WareHouse)
                    .WithMany(p => p.WareHouseDetails)
                    .HasForeignKey(d => d.WareHouseId)
                    .HasConstraintName("FK_WareHouseDetail_WareHouse");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
