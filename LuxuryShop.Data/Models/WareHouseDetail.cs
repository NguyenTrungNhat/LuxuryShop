using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class WareHouseDetail
    {
        public int WareHouseDetailId { get; set; }
        public int? WareHouseId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product? Product { get; set; }
        public virtual WareHouse? WareHouse { get; set; }
    }
}
