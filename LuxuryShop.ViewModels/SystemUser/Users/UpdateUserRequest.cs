using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.SystemUser.Users
{
    public class UpdateUserRequest
    {
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Gender { get; set; }
        public string?  Thumb { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
