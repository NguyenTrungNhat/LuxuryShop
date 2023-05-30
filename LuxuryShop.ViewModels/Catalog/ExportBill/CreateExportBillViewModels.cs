using LuxuryShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Catalog.ExportBill
{
    public class CreateExportBillViewModels
    {
        public string CustomerId { get; set; }
        public List<ExportBillDetail> ExportBillDetails { get; set; }
    }
}
