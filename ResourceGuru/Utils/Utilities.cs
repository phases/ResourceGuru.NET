using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Utils
{
    public class Utilities
    {
        public static string FormatDateForRequest(DateTime? date)
        {
            if (date.HasValue)
                return date.Value.ToString("yyyy-MM-dd");
            return null;
        }
    }
}
