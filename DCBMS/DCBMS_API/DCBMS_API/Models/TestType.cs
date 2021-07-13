using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Models
{
    public class TestType
    {
        public int Id { get; set; }
        [Required]
        public string TestTypeName { get; set; }
    }
}
