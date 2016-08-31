using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityCore;
using Repository;

namespace ShoppingCart.Controllers
{
    public class CheckoutController : Controller
    {
        UnitOfWork _uow = new UnitOfWork();

        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddressAndPayment()
        {
            if(this.HttpContext.User.Identity.Name!=string.Empty)
            return View();
            else
            {
                return  RedirectToAction("Login", "Account", new { ReturnUrl =Url.Action("AddressAndPayment", "Checkout") });           
            }
        }
        [HttpPost]
        public ActionResult AddressAndPayment(Order _order)
        {
            var CART = BusinessCore.ShoppingCart.GetCart(this.HttpContext);
            var order = new Order();
            order.OrderId = Guid.NewGuid();
            order.OrderDate = _order.OrderDate;
            order.Username = this.HttpContext.User.Identity.Name;
            order.Address = _order.Address;
            order.City = _order.City;
            order.Country = _order.Country;
            order.Email = _order.Email;
            order.FirstName = _order.FirstName;
            order.LastName = _order.LastName;
            order.Phone = _order.Phone;
            order.PostalCode = _order.PostalCode;
            order.State = _order.State;
            order.Total = CART.GetTotal();
            _uow.Repository<Order>().Add(order);
            _uow.Save();

            var cart = BusinessCore.ShoppingCart.GetCart(this.HttpContext);
            var lstCart = cart.GetCartItems();

            foreach(var item in lstCart)
            {
                var orderDetail = new OrderDetail
                {
                    OrderDetailId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ItemId =item.ItemId,
                    Quantity=item.Count,
                    UnitPrice=item.ItemMaster.Price  
               };
                _uow.Repository<OrderDetail>().Add(orderDetail);
                _uow.Save();
            }

            var cartCollection = _uow.Repository<Cart>().Get().Where(c => c.CartId == this.HttpContext.User.Identity.Name);

            foreach (var item in cartCollection)

            {
                _uow.Repository<Cart>().Delete(item.RecordId);
                _uow.Save();
            }
       

           return RedirectToAction("Complete",  new { id = order.OrderId});
        }


        public ActionResult Complete(Guid id)
        {
            // Validate customer owns this order
            bool isValid = _uow.Repository<Order>().Get().Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}