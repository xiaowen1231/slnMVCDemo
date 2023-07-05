using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            var db = new dbDemoEntities();
            var data = from p in db.tProduct
                       select p;
            return View(data);
        }

        public ActionResult AddToCart(int? id)
        {
            ViewBag.fId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart()
        {
            return RedirectToAction("List");
        }
    }
}