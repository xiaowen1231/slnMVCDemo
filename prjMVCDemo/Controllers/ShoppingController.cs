using prjMVCDemo.Models;
using prjMVCDemo.ViewModels;
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

        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SessionKey_ShoppingCartList] 
                as List<CShoppingCartItem>;
            if (cart == null)
            {
                return RedirectToAction("List");
            }
            return View(cart);
        }

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
        public ActionResult AddToCart(CAddToCartViewModel vm)
        {
            var db = new dbDemoEntities();
            var product = db.tProduct.FirstOrDefault(p => p.fId == vm.fId);
            if (product != null)
            {
                tShoppingCart shoppingCart = new tShoppingCart();
                shoppingCart.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                shoppingCart.fCustomerId = 1;
                shoppingCart.fProductId = product.fId;
                shoppingCart.fCount = vm.fCount;
                shoppingCart.fPrice = product.fPrice;

                db.tShoppingCart.Add(shoppingCart);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
        public ActionResult AddToSession(int? id)
        {
            ViewBag.fId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vm)
        {
            var db = new dbDemoEntities();
            var product = db.tProduct.FirstOrDefault(p => p.fId == vm.fId);
            if (product != null)
            {
                List<CShoppingCartItem> cart = Session[CDictionary.SessionKey_ShoppingCartList] as List<CShoppingCartItem>;

                if (cart == null)
                {
                    cart = new List<CShoppingCartItem>();
                    Session[CDictionary.SessionKey_ShoppingCartList] = cart;
                }

                CShoppingCartItem item = new CShoppingCartItem();
                item.productId = product.fId;
                item.count = vm.fCount;
                item.price = (decimal)product.fPrice;
                item.product = product;

                cart.Add(item);
            }

            return RedirectToAction("List");
        }

    }
}