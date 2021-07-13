using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Models.ViewModel
{
    public class TestWiseReportVM
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int NoOfTest { get; set; }
        public decimal TotalAmount { get; set; }

    }

    public class TypeWiseReportVM
    {
        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; }
        public int NoOfTest { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
