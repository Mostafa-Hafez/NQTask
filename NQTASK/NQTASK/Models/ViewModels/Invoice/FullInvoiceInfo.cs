using NQTASK.Models.ViewModels.Product;
using System;
using System.Collections.Generic;

namespace NQTASK.Models.ViewModels.Invoice
{
    public class FullInvoiceInfo
    {
        public int Id { get; set; }
        public string code { get; set; }
        public DateTime DateofRegistration { get; set; }
        public string customerName { get; set; }
        //public List<ProductInfoVM> products { get; set; }  

    }
}
