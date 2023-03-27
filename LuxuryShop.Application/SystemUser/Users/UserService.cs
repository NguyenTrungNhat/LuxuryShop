using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Data.Models;
using LuxuryShop.Utilities.Exceptions;
using LuxuryShop.ViewModels.Catalog.Products;
using LuxuryShop.ViewModels.SystemUser.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LuxuryShop.Application.SystemUser.Users
{
    public class UserService : IUserService
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly AppSettings _appSettings;
        public UserService(IDatabaseHelper dbHelper, IOptions<AppSettings> appSettings)
        {
            _dbHelper = dbHelper;
            _appSettings = appSettings.Value;
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
                var user = dt.ConvertTo<UserViewModel>().FirstOrDefault();
                if (user == null)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
            if (!IsValidPassword(request.Password))
            {
                return false;
            }
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_Accounts_Users_create",
                "@Birthday", request.BirthDay,
                "@Gender", request.Gender,
                "@Thumb", request.Thumb,
                "@Address", request.Address,
                "@Email", request.Email,
                "@Phone", request.Phone,
                "@FullName", request.FullName,
                "@UserName", request.UserName,
                "@Password", request.Password,
                "@RoleID", request.RoleID);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new LuxuryShopException(Convert.ToString(result) + msgError);
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsValidPassword(string plainText)
        {
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match match = regex.Match(plainText);
            return match.Success;
        }

        public List<UserViewModel> GetUsersPaging(int pageIndex, int pageSize, out long total)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_getUsertoPaging",
                    "@page_index", pageIndex,
                    "@page_size", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<UserViewModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
