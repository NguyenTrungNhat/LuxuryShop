using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.ViewModels.Common
{
    public class ResponseRequestBase 
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
