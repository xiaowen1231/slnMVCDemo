using prjMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            List<CCustomer> x = new CCustomerFactory().queryAll();
            return View(x);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            
            new CCustomerFactory().create(x);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id) 
        {
            if(id!=null)
            {
                new CCustomerFactory().delete((int)id);
            }
            return RedirectToAction("List");
        }
    }
}