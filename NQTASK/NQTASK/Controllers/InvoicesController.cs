using Microsoft.AspNetCore.Mvc;
using NQTASK.Models;
using NQTASK.Models.ViewModels.Client;
using NQTASK.Models.ViewModels.Invoice;
using NQTASK.Models.ViewModels.Product;
using NQTASK.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NQTASK.Controllers
{
    public class InvoicesController : Controller
    {
        private AppDbContext _context;
        public InvoicesController(AppDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            List<ClientInvoiceforlistVM> invoices = new List<ClientInvoiceforlistVM>();
            foreach (var inv in _context.Invoices.ToList())
            {
                ClientInvoiceforlistVM invoice= new ClientInvoiceforlistVM();
                invoice.In_Id=inv.In_Id;
                invoice.Cl_Id=inv.cl_Id;
                invoice.OutDateTime=inv.OutDateTime;
                invoice.Total=inv.Total;
                var client = _context.Clients.FirstOrDefault(x => x.Cl_ID == inv.cl_Id);
                invoice.CustomerName = client.Name;

                invoices.Add(invoice);


            }
            return View(invoices);
        }


        public ActionResult ShowInvoice(ClientInvoiceforlistVM invoice)
        {
            try
            {

                List<UserInvoices> invoices = Getinvoices(invoice);
                ViewBag.Cl_Id=invoice.Cl_Id.ToString();

                return View(invoices);
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }



        public List<UserInvoices> Getinvoices(ClientInvoiceforlistVM invoice)
        {

            var client = _context.Clients.FirstOrDefault(z => z.Cl_ID ==invoice.Cl_Id );
            if (client == null) { return new List<UserInvoices>(); }
            List<UserInvoices> userinvoices = new List<UserInvoices>();
            var listinvoices = _context.Invoices.Where(c => c.cl_Id == client.Cl_ID);

            if (listinvoices == null) { return new List<UserInvoices>(); }

            foreach (var item in listinvoices)
            {
                UserInvoices userinvoice = new UserInvoices();
                userinvoice.Total = item.Total;
                userinvoice.OutDatetime = item.OutDateTime;
                userinvoice.Products = new List<ProductInfoVM>();

                List<Invoice_Product> invoicesproducts = _context.Invoice_Product.Where(x => x.In_ID == item.In_Id).ToList();
                foreach (var item2 in invoicesproducts)
                {
                    Product product = _context.Products.FirstOrDefault(a => a.Pr_ID == item2.Pr_ID);
                    if (product != null)
                    {
                        ProductInfoVM productInfo = new ProductInfoVM();
                        productInfo.Name = product.Name;
                        productInfo.Price = product.Price;

                        userinvoice.Products.Add(productInfo);
                    }


                }
                userinvoices.Add(userinvoice);

            }
            return userinvoices;
        }



        public ActionResult PrintUserInvoice(int id)
        {

            FullinvoiceReport fullrep = new FullinvoiceReport();


            var client = _context.Clients.FirstOrDefault(z => z.Account_Id == id);
            if (client == null) { return View(new List<Invoice>()); }



            List<Invoice> invoices = _context.Invoices.Where(z => z.cl_Id == id).ToList();
            ClientInfo cl = new ClientInfo();
            FullInvoiceInfo fullInvoiceInfo = new FullInvoiceInfo();
            cl.ID = client.Cl_ID;
            cl.Code = client.Code;
            cl.DateofRegistration = client.DatetimeofRegistration;
            cl.Name = client.Name;

            List<ProductInfoVM> products = new List<ProductInfoVM>();
            foreach (Invoice invoice in invoices)
            {
                var prods = _context.Invoice_Product.Where(x => x.In_ID == invoice.In_Id);
                foreach (var product in prods)
                {
                    var pro = new Product();
                    pro = _context.Products.FirstOrDefault(c => c.Pr_ID == product.Pr_ID);
                    if (pro != null)
                    {
                        products.Add(new ProductInfoVM() { Name = pro.Name, Price = pro.Price });
                    }
                }

            }


            fullrep.DataSource = products;
            fullrep.Parameters["DateOfReg"].Value = cl.DateofRegistration.ToString();
            fullrep.Parameters["CName"].Value = cl.Name.ToString();
            fullrep.Parameters["Code"].Value = cl.Code;
            fullrep.Parameters["ID"].Value = cl.ID;

            return View(fullrep);
        }

    }


}
