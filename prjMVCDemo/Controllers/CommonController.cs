using prjMVCDemo.Models;
using prjMVCDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            if (Session[CDictionary.SessionKey_LoginCustomer] == null)
            {
                return RedirectToAction("Login");
            }
            
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomerFactory factory = new CCustomerFactory();
            CCustomer customer = factory.authenticated(vm.txtAccount, vm.txtPassword);
            if (customer != null && customer.fPassword.Equals(vm.txtPassword))
            {
                Session[CDictionary.SessionKey_LoginCustomer] = customer;
                return RedirectToAction("Home"); 
            }
            return View();
        }

    }
}