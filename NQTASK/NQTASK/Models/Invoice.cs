using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NQTASK.Models
{
    public class Invoice
    {
        [Key]
        public int In_Id { get; set; }
        public DateTime OutDateTime { get; set; }
        public decimal Total { get; set; }

        public int cl_Id { get; set; }

        [ForeignKey("cl_Id")]
        public virtual Client Client { get; set; }
        public virtual List<Invoice_Product> InvoicesProducts { get; set; }
    }
}
