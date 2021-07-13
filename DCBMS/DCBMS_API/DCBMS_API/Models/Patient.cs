using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string BillNo { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public bool IsPaid { get; set; }
        public bool IsComplete { get; set; }
        public bool IsDelivered { get; set; }

    }
}
