using LuxuryShop.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.System.Users
{
    public  interface IUserService
    {
        string Authenticate(LoginRequest request);
        bool Register(RegisterRequest request);
    }
}
