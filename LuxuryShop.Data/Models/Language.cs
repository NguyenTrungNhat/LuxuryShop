using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Language
    {
        public Language()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            ProductTranslations = new HashSet<ProductTranslation>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsDefault { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<ProductTranslation> ProductTranslations { get; set; }
    }
}
