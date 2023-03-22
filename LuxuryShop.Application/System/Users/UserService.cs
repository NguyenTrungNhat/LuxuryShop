using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Data.Models;
using LuxuryShop.ViewModels.Catalog.Products;
using LuxuryShop.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly IConfiguration _config;
        public UserService(IDatabaseHelper dbHelper, IConfiguration config) 
        {
            _dbHelper = dbHelper;
            _config = config;
        }

        public string Authenticate(LoginRequest request)
        {

            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Accounts_User_CheckLogin",
                     "@UserName", request.UserName,
                     "@Password", request.Password);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                var user =  dt.ConvertTo<UserViewModel>().FirstOrDefault();
                if (user == null)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.FullName.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleName),
                    new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup, user.Password)
                    }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tmp = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(tmp);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
