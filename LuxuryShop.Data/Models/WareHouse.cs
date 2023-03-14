using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class WareHouse
    {
        public WareHouse()
        {
            CheckWhs = new HashSet<CheckWh>();
            WareHouseDetails = new HashSet<WareHouseDetail>();
        }

        public int WareHouseId { get; set; }
        public string? WareHouseName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<CheckWh> CheckWhs { get; set; }
        public virtual ICollection<WareHouseDetail> WareHouseDetails { get; set; }
    }
}
