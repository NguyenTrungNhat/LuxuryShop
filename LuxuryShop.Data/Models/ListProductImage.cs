using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class ListProductImage
    {
        public int ListProductImageId { get; set; }
        public int? ProductId { get; set; }
        public string? ImagePath { get; set; }
        public string? Caption { get; set; }
        public bool? IsDefault { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? SortOrder { get; set; }
        public long? FileSize { get; set; }

        public virtual Product? Product { get; set; }
    }
}
