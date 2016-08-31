using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using EntityCore;
using ShoppingCart.ViewModels;
 

namespace ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        
     
        // GET: ShoppingCart
        public ActionResult Index()
        {

            var soppinCart = BusinessCore.ShoppingCart.GetCart(this.HttpContext);

            var vmSoppinCart = new ShoppingCartViewModel
            {
                CartItems = soppinCart.GetCartItems(),
                CartTotal = soppinCart.GetTotal() 
            };
            return View(vmSoppinCart);
        }
     [HttpGet]   
        public ActionResult AddToCart(Guid id)
        {
            var addedItem = _uow.Repository<ItemMaster>().GetById(id);
                     
            // Add it to the shopping cart
            var cart =BusinessCore.ShoppingCart.GetCart(this.HttpContext);
            
            cart.AddToCart(addedItem);

            return RedirectToAction("Index", "ShoppingCart");

        }

        public ActionResult CartSummary()
        {
            var cart = BusinessCore.ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
        [HttpPost]
        public ActionResult RemoveFromCart(Guid id)
        {
            // Remove the item from the cart
            var cart =BusinessCore.ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            //string albumName = storeDB.Carts
            //    .Single(item => item.RecordId == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            //var results = new ShoppingCartRemoveViewModel
            //{
            //    Message = Server.HtmlEncode(albumName) +
            //        " has been removed from your shopping cart.",
            //    CartTotal = cart.GetTotal(),
            //    CartCount = cart.GetCount(),
            //    ItemCount = itemCount,
            //    DeleteId = id
            //};

            return Json(new { success = true });
        }

   

    }
}