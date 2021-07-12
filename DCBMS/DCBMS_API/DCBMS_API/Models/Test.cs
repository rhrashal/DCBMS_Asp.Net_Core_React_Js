using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public decimal Fee { get; set; }
        [Required]
        [ForeignKey("TestType")]
        public int TestTypeId { get; set; }
        public virtual TestType TestType { get; set; }
    }
}
