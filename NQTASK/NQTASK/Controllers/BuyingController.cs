using Microsoft.AspNetCore.Http;
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
    public class BuyingController : Controller
    {
        private readonly AppDbContext _context;
       
        public BuyingController(AppDbContext context)
        {
                _context = context;
        }

        public ActionResult Index()
        {

            try
            {

            string userid= Request.Cookies["UserID"];
            string username= Request.Cookies["UserName"];
            
            if (userid != null) 
            { 
                var acc = _context.Accounts.FirstOrDefault(z=>z.Acc_ID==int.Parse(userid));
                if (acc== null || username!=acc.UserName) { 
                    return RedirectToAction("Login","Account");
                  }
                var client= _context.Clients.FirstOrDefault(x=>x.Account_Id==acc.Acc_ID);
                if (client == null)
                {
                    return RedirectToAction("Registration", "Account");
                }
                ClientInfo clinfo= new ClientInfo();
                    clinfo.ID= acc.Acc_ID;
                    clinfo.Name = client.Name;
                    clinfo.Code= client.Code;
                    clinfo.DateofRegistration = client.DatetimeofRegistration;

                    
                    List<productselectVM> products= new List<productselectVM>();
                    foreach (var pro in _context.Products.ToList())
                    {
                        productselectVM psvm = new productselectVM();
                        psvm.ID=pro.Pr_ID;
                        psvm.Name = pro.Name;
                        products.Add(psvm);
                    }
                    ViewBag.products=products;

                    return View(clinfo);

                   

            }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {

                return RedirectToAction("Login", "Account");
            }

        }

        
        [HttpGet]
        public ActionResult selectproduct(int? id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(z => z.Pr_ID == id);
                if (product == null)
                {
                    return PartialView("_Products", new ProductInfoVM());
                }
                var prinfo = new ProductInfoVM();
                prinfo.Name = product.Name;
                prinfo.Price = product.Price;

                return PartialView("_Products", prinfo);
            }
            catch 
            {
                return PartialView("_Products", new ProductInfoVM());

            }
           
        }

        [HttpPost]
        public ActionResult Save([FromBody] ProductRequest request)
        {
            try
            {

            
            string userid = Request.Cookies["UserID"];
            if (userid == null) { return RedirectToAction("Login", "Account"); }
            var client = _context.Clients.FirstOrDefault(z=>z.Account_Id==int.Parse(userid));
            if(client == null) { return RedirectToAction("Login", "Account"); }

            Invoice invoic= new Invoice();
            invoic.cl_Id = client.Account_Id;
            invoic.OutDateTime = DateTime.Now;
            decimal total = 0;
            invoic.Total = total;
            _context.Invoices.Add(invoic);
            _context.SaveChanges();

            foreach (var name in request.Products)
            {
                var product=_context.Products.FirstOrDefault(z=>z.Name == name);
                if(product != null)
                {
                    total+= product.Price;

                    Invoice_Product invoice_Product = new Invoice_Product();
                    invoice_Product.Pr_ID=product.Pr_ID;
                    invoice_Product.In_ID=invoic.In_Id;
                    invoice_Product.invoice=invoic;
                    invoice_Product.product=product;

                    _context.Invoice_Product.Add(invoice_Product);

                    _context.SaveChanges();

                }
            }
            invoic.Total=total;
            _context.Invoices.Update(invoic);
            _context.SaveChanges();

           return Ok();
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }

        }


        [HttpGet]
        public ActionResult AllInvoices()
        {
            try
            {
                int id = 0;
                 List<UserInvoices> invoices =Getinvoices(out id);
                ViewBag.cl_ID = id.ToString();
                return View (invoices);
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }

        public ActionResult PrintInvoice()
        {

             FullinvoiceReport fullrep= new FullinvoiceReport(); 

           
            string userid = Request.Cookies["UserID"];
            if (userid == null) { return View( new List<Invoice>()); }
            var client = _context.Clients.FirstOrDefault(z => z.Account_Id == int.Parse(userid));
            if (client == null) { return View(new List<Invoice>()); }

            

            List<Invoice> invoices =_context.Invoices.Where(z=>z.cl_Id== int.Parse(userid)).ToList();
            ClientInfo cl = new ClientInfo();
            FullInvoiceInfo fullInvoiceInfo = new FullInvoiceInfo();
            cl.ID = client.Cl_ID;
            cl.Code= client.Code;
            cl.DateofRegistration = client.DatetimeofRegistration;
            cl.Name = client.Name;

            List<ProductInfoVM> products= new List<ProductInfoVM>();
            foreach (Invoice invoice in invoices) 
            {
                var prods = _context.Invoice_Product.Where(x => x.In_ID == invoice.In_Id);
                foreach (var product in prods) 
                {
                    var pro = new Product();
                    pro=_context.Products.FirstOrDefault(c=>c.Pr_ID==product.Pr_ID);
                    if(pro != null) {
                       products.Add(new ProductInfoVM() { Name=pro.Name,Price=pro.Price});
                    }
                }
                    
            }


            fullrep.DataSource= products;
            fullrep.Parameters["DateOfReg"].Value = cl.DateofRegistration.ToString();
            fullrep.Parameters["CName"].Value= cl.Name.ToString();
            fullrep.Parameters["Code"].Value = cl.Code;
            fullrep.Parameters["ID"].Value= cl.ID;
           
            return View(fullrep);
        }




        public List<UserInvoices> Getinvoices(out int id)
        {
            id = 0;
            string userid = Request.Cookies["UserID"];
            if (userid == null) {  return new List<UserInvoices>(); }
            var client = _context.Clients.FirstOrDefault(z => z.Account_Id == int.Parse(userid));
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
            id = client.Cl_ID;
            return userinvoices;
        }








        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
