using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class CategoryTranslation
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoTitle { get; set; }
        public string LanguageId { get; set; } = null!;
        public string SeoAlias { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual Category Cat { get; set; } = null!;
        public virtual Language Language { get; set; } = null!;
    }
}
