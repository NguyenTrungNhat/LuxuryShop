using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.SystemUser.Users
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Thumb { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }

    }
}
