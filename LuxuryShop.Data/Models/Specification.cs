using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Specification
    {
        public int SpecificationId { get; set; }
        public int? ProductId { get; set; }
        public string? SpecificationName { get; set; }
        public string? Description { get; set; }

        public virtual Product? Product { get; set; }
    }
}
