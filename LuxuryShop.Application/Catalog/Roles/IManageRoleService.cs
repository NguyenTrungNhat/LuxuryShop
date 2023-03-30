using LuxuryShop.Data.Models;
using LuxuryShop.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Application.Catalog.Roles
{
    public interface IManageRoleService
    {
        List<Role> GetAll();
    }
}
