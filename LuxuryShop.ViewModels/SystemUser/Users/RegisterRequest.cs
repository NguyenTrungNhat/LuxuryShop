using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.SystemUser.Users
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string? Thumb { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}
