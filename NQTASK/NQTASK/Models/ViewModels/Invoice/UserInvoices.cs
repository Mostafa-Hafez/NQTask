using Microsoft.EntityFrameworkCore.Infrastructure;
using NQTASK.Models.ViewModels.Product;
using System;
using System.Collections.Generic;

namespace NQTASK.Models.ViewModels.Invoice
{
    public class UserInvoices
    {

        
        public  DateTime OutDatetime { get; set; }
        public decimal Total { get; set; }
        public List<ProductInfoVM> Products { get; set; }
    }
}
