using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NQTASK.Models;
using NQTASK.Models.ViewModels.Account;
using NQTASK.Models.ViewModels.Client;
using System;
using System.Linq;

namespace NQTASK.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context=context;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Registration(CreateClientWithAccountVM AccVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    

                    var existusername=_context.Accounts.FirstOrDefault(z=>z.UserName==AccVM.UserName);
                    if (existusername != null) 
                    {
                        ViewBag.error = "This username has assined before try with another";
                        return View(AccVM);
                        
                    }
                    var existclindCode = _context.Clients.FirstOrDefault(z => z.Code == AccVM.Code);
                    if (existclindCode != null)
                    {
                        ViewBag.error = "This Code has assined before try with another";
                        return View(AccVM);

                    }
                    if (AccVM.Password != AccVM.confirmPassword) 
                    {
                        ViewBag.error = "Invalid confirmation password";
                        return View(AccVM);
                    }

                    Account acc = new Account();
                    Client newcl = new Client();
                    acc.UserName=AccVM.UserName;
                    acc.Password=AccVM.Password;
                    acc.Role = Roles.Client;
                    
                    newcl.Code=AccVM.Code;
                    newcl.DatetimeofRegistration=DateTime.Now;
                    newcl.Name = AccVM.Name;
                    

                    _context.Accounts.Add(acc);
                    _context.SaveChanges();
                   
                    newcl.Account_Id=acc.Acc_ID;
                    newcl.Account=acc;

                    _context.Clients.Add(newcl);
                    _context.SaveChanges();

                    return RedirectToAction("Login");

                }
                ViewBag.error = "There is an error in your data please try again";
                return View(AccVM);
            }
            catch 
            {
                ViewBag.error = "there is an error please try Later";
                return View(AccVM);
               
            }
           
            
        }



        [HttpGet]
        public IActionResult NewAdmin() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult NewAdmin(CreateAdminAccount adaccVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(adaccVM);
                }
                var existusername = _context.Accounts.FirstOrDefault(z => z.UserName == adaccVM.UserName);
                if (existusername != null)
                {
                    ViewBag.error = "This username was taken before try eith another one";
                    return View(adaccVM);

                }
                if (adaccVM.Password != adaccVM.confirmPassword)
                {
                    return View(adaccVM);
                }
                Account adminaccount=new Account();
                adminaccount.UserName=adaccVM.UserName;
                adminaccount.Password=adaccVM.Password;
                adminaccount.Role=Roles.Admin;

                _context.Accounts.Add(adminaccount);
                _context.SaveChanges();
                ViewBag.result = "Admin Added succefully";
                return View();
            }
            catch 
            {
                ViewBag.error = "Try again Later";
               return View(adaccVM) ;
            }
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "please enter valid username and password";
                    return View(loginVM);
                }
                var account = _context.Accounts.FirstOrDefault(z => z.UserName == loginVM.UserName && z.Password == loginVM.Password);
                if (account == null) 
                {
                    ViewBag.error = "User Name or password incorrect";
                    return View(loginVM) ;
                }

                AssignCokkies(account.Acc_ID, account.UserName);
                
                if (account.Role == Roles.Admin)
                {
                    Response.Cookies.Append("Role","Admin",options);
                    return RedirectToAction("Index", "Invoices");
                }
                else
                {
                    Response.Cookies.Append("Role", "Client", options);
                    return RedirectToAction("Index", "Buying");
                }

            }
            catch 
            {

                return View(loginVM);
            }
            
        }


        private CookieOptions options = new CookieOptions()
        {
            Domain = "localhost",
            Path = "/",
            Expires = DateTime.Now.AddDays(1),
            Secure = false,
            HttpOnly = true,
            IsEssential = true
        };

        public void AssignCokkies(int id,string username)
        {
            

            Response.Cookies.Append("UserID", id.ToString(), options);
    

            Response.Cookies.Append("UserName", username, options);
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("UserID");

            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("Role");

            return RedirectToAction("Login");
        }
    }
}
