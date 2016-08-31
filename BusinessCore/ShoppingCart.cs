using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EntityCore;
using Repository;

namespace BusinessCore
{
   public class ShoppingCart
    {
        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        UnitOfWork _uow = new UnitOfWork();

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public  void AddToCart(ItemMaster item)
        {
            var cartItem = _uow.Repository<Cart>().Get().Where(c => c.CartId == ShoppingCartId && c.ItemId == item.Id).FirstOrDefault();

            if(cartItem == null)
            {
                cartItem = new Cart
                {
                    CartId = ShoppingCartId,
                    RecordId = Guid.NewGuid(),
                    ItemId = item.Id,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _uow.Repository<Cart>().Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            _uow.Save();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _uow.Repository<Cart>().Get()
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }
        public List<Cart> GetCartItems()
        {
            return _uow.Repository<Cart>().Get().Where(c => c.CartId == ShoppingCartId).ToList();
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _uow.Repository<Cart>().Get()
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.ItemMaster.Price).Sum();
            return total ?? decimal.Zero;
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _uow.Repository<Cart>().Get().Where(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _uow.Save();
        }
        public int RemoveFromCart(Guid id)
        {
            // Get the cart
            var cartItem = _uow.Repository<Cart>().Get().Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {                    
                    _uow.Repository<Cart>().Delete(cartItem.RecordId);
                }

                // Save changes
                _uow.Save();
            }

            return itemCount;
        }


    }
}
