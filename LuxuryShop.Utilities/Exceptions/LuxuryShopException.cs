using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryShop.Utilities.Exceptions
{
    public class LuxuryShopException : Exception
    {
        public LuxuryShopException() { }
        public LuxuryShopException(string message) : base(message) { }
        public LuxuryShopException(string message, Exception innerException) : base(message,innerException) { }
    }
}
