using LuxuryShop.ViewModels.SystemUser.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.SystemUser.Users
{
    public interface IUserService
    {
        string Authenticate(LoginRequest request);
        bool Register(RegisterRequest request);
    }
}
