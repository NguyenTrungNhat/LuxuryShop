using LuxuryShop.Application.Common;
using LuxuryShop.Data.Helper;
using LuxuryShop.Data.Helper.Interfaces;
using LuxuryShop.Data.Models;
using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Roles
{
    public class ManageRoleService : IManageRoleService
    {
        private readonly IDatabaseHelper _dbHelper;

        public ManageRoleService(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<Role> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_Role_GetAll");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<Role>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
