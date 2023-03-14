﻿using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class Sale
    {
        public int SaleId { get; set; }
        public int? ProductId { get; set; }
        public double? Percentt { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public bool? Status { get; set; }

        public virtual Product? Product { get; set; }
    }
}
