using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class CheckWh
    {
        public CheckWh()
        {
            CheckWhdetails = new HashSet<CheckWhdetail>();
        }

        public int CheckId { get; set; }
        public int? UserId { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? Status { get; set; }
        public int? WareHouseId { get; set; }
        public string? Description { get; set; }

        public virtual User? User { get; set; }
        public virtual WareHouse? WareHouse { get; set; }
        public virtual ICollection<CheckWhdetail> CheckWhdetails { get; set; }
    }
}
