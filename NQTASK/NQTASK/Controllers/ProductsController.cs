using Microsoft.AspNetCore.Mvc;
using NQTASK.Models;
using NQTASK.Models.ViewModels.Product;
using System.Collections.Generic;
using System.Linq;

namespace NQTASK.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductforListVM> products = new List<ProductforListVM>();
            foreach (var product in _context.Products)
            {
                products.Add( new ProductforListVM() {Id=product.Pr_ID,Name=product.Name,Price=product.Price });

            }
            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(NewProductVM pro)
        {
            if(!ModelState.IsValid) { return View(pro); }
            if (pro == null) 
            { return RedirectToAction("Index"); }
            try
            {
                Product product = new Product();
                product.Name = pro.Name;
                product.Price = pro.Price;
                _context.Products.Add(product); 
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            {
                ViewBag.error = "Some Thing is error Please try Later";
                return View(pro);
                
            }
            
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            try
            {
                if (id < 0) { return RedirectToAction("Index"); }
               
                Product product =_context.Products.FirstOrDefault(z=>z.Pr_ID == id);
                if (product == null) { return RedirectToAction("Index"); }
                ProductforListVM pro= new ProductforListVM();
                pro.Id=product.Pr_ID;
                pro.Name= product.Name;
                pro.Price= product.Price;

                return View(pro);
            }
            catch 
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult UpdateProduct(ProductforListVM pro)
        {
            if(pro == null) { return RedirectToAction("Index"); }
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            try
            {
                Product product = _context.Products.FirstOrDefault(z => z.Pr_ID == pro.Id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                product.Name= pro.Name;
                product.Price= pro.Price;
                _context.Products.Update(product); 
                _context.SaveChanges();

                    return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }

        }


        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if (id < 0) { return RedirectToAction("Index"); }
            try
            {
                Product pro= _context.Products.FirstOrDefault(z=>z.Pr_ID==id);
                if(pro == null) { return RedirectToAction("Index"); }
                _context.Products.Remove(pro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("Index");

            }
        }
    }
}
