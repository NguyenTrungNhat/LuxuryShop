using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class ProductTranslation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoTitle { get; set; }
        public string LanguageId { get; set; } = null!;
        public string SeoAlias { get; set; } = null!;

        public virtual Language Language { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
