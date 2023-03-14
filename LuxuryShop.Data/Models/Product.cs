using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            AttributesPrices = new HashSet<AttributesPrice>();
            CheckWhdetails = new HashSet<CheckWhdetail>();
            ImportBillDetails = new HashSet<ImportBillDetail>();
            ListProductImages = new HashSet<ListProductImage>();
            OrderDetails = new HashSet<OrderDetail>();
            PriceHistories = new HashSet<PriceHistory>();
            ProductTranslations = new HashSet<ProductTranslation>();
            PromotionsDetails = new HashSet<PromotionsDetail>();
            Sales = new HashSet<Sale>();
            Specifications = new HashSet<Specification>();
            WareHouseDetails = new HashSet<WareHouseDetail>();
        }

        public int ProductId { get; set; }
        public string? ShortDesc { get; set; }
        public int? CatId { get; set; }
        public int? Discount { get; set; }
        public string? Thumb { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool BestSellers { get; set; }
        public bool HomeFlag { get; set; }
        public bool Active { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Category? Cat { get; set; }
        public virtual ICollection<AttributesPrice> AttributesPrices { get; set; }
        public virtual ICollection<CheckWhdetail> CheckWhdetails { get; set; }
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
        public virtual ICollection<ListProductImage> ListProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual ICollection<ProductTranslation> ProductTranslations { get; set; }
        public virtual ICollection<PromotionsDetail> PromotionsDetails { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Specification> Specifications { get; set; }
        public virtual ICollection<WareHouseDetail> WareHouseDetails { get; set; }
    }
}
