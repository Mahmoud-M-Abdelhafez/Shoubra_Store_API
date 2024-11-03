using WebAppStore.Data;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly StoreContextDB db;
        public CartRepository(StoreContextDB context)
        {
            db = context;
        }

        
        public CartProductsVM GetAll(string id)
        {
            CartProductsVM cartProductsVM = new CartProductsVM();
            cartProductsVM.carts = db.Carts.Where(p => p.UserId == id).ToList();
            cartProductsVM.products = db.Products.ToList();
            cartProductsVM.images = db.ProductImages.ToList();

            return cartProductsVM;
        }

        public CartDetailsVM GetById(int id)
        {
            CartDetailsVM cartDetailsVM = new CartDetailsVM();
            cartDetailsVM.cart = db.Carts.SingleOrDefault(c => c.Id == id);
            cartDetailsVM.product = db.Products.SingleOrDefault(p => p.Id == cartDetailsVM.cart.ProductId);
            List<ProductImage> Images = db.ProductImages.Where(p => p.ProductId == cartDetailsVM.cart.ProductId).ToList();
            cartDetailsVM.productImage = Images.First();
          
            return cartDetailsVM;
        }

        public void Insert(AddCartVM item)
        {

            int prodid = item.ProductId;
            var cart = new Cart
            {
                UserId = item.UserId,
                ProductId = prodid,
                Qty = item.Qty,
            };

            db.Carts.Add(cart);
            db.SaveChanges();
        }

        public void Edit(int id, AddCartVM item)
        {
            var cart = db.Carts.SingleOrDefault(c => c.Id == id);
          
            cart.Qty = item.Qty;
            db.SaveChanges();
        }

        public string Delete(int id)
        {
            Cart cart = db.Carts.SingleOrDefault(c => c.Id == id);
            string UserId = cart.UserId;
            db.Carts.Remove(cart);
            db.SaveChanges();
            return UserId;
        }
    }
}
