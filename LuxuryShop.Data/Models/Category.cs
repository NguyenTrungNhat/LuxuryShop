using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            Products = new HashSet<Product>();
        }

        public int CatId { get; set; }
        public string? CatName { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }
        public bool Published { get; set; }
        public string? Thumb { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
