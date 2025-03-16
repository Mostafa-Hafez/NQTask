using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NQTASK.Models
{
    public class Client
    {
        [Key]
        public int Cl_ID { get; set; }

        
        public string Code { get; set; } //unique
        public string Name { get; set; }
        public DateTime DatetimeofRegistration { get; set; }

        
        public int Account_Id { get; set; }
            [ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }

        public virtual List<Invoice> Invoices { get; set; }
    }
}
