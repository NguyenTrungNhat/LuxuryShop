using System;
using System.Collections.Generic;

namespace LuxuryShop.Data.Models
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            CheckWhs = new HashSet<CheckWh>();
            ImportBills = new HashSet<ImportBill>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? Thumb { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CheckWh> CheckWhs { get; set; }
        public virtual ICollection<ImportBill> ImportBills { get; set; }
    }
}
