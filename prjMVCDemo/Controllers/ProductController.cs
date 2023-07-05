using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            IEnumerable<tProduct> datas = null;
            dbDemoEntities db = new dbDemoEntities();
            if (string.IsNullOrEmpty(keyword)) 
            { 
                datas = from t in db.tProduct
                        select t;
            }
            else
            {
                datas = db.tProduct.Where(p => p.fName.Contains(keyword));
            }
            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            var db = new dbDemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if(id!=null)
            {
                dbDemoEntities db = new dbDemoEntities();
                tProduct prod = db.tProduct.FirstOrDefault(p=>p.fId==id);
                if(prod!=null)
                {
                    db.tProduct.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(x=>x.fId==id);
            
            return View(prod);
        }

        [HttpPost]
        public ActionResult Edit(tProduct productInForm)
        {
            var db = new dbDemoEntities();
            tProduct productInDb = db.tProduct.FirstOrDefault(x => x.fId == productInForm.fId);
            if (productInDb!=null)
            {
                productInDb.fName = productInForm.fName;
                productInDb.fQty = productInForm.fQty;
                productInDb.fCost = productInForm.fCost;
                productInDb.fPrice = productInForm.fPrice;

                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}