using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Models.ViewModel
{
    public class ScerchVM
    {
        public string Mobile { get; set; }
        public string BillNo { get; set; }
    }

    public class FilterVM
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
