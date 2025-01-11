using Eticaret.Service.Abstract;
using Eticaret.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Eticaret.WebUI.ExtensionMethods;
using Eticaret.WebUI.Models;
using ServiceStack;
using Eticaret.Service.Concrete;

namespace Eticaret.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<AppUser> _serviceProduct;

        public CartController(IService<AppUser> serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        // Sepet sayfasını göstermek için
        public IActionResult Index()
        {
            var cart = GetCart();   
            var cartViewModel = new CartViewModel
            {
                CartLines = cart.GetCartItems()
            };

            return View(cartViewModel);
        }

        // Sepete ürün eklemek için
        public IActionResult Add(int productId, int quantity = 1)
        {
            var product = _serviceProduct.Find(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity);
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // Sepetten ürün kaldırmak için
        public IActionResult Remove(int productId)
        {
            var product = _serviceProduct.Find(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            var cart = GetCart();
            var cartViewModel = new CheckoutViewModel
            {
                CartProducts = cart.GetCartItems()
            };

            return View(cartViewModel);
        }

        // Sepet verisini session'dan almak
        private CartService GetCart()
        {
            return HttpContext.Session.GetJson<CartService>("Cart") ?? new CartService();
        }
    }

    public interface IService<T>
    {
        object GetAllAsync { get; set; }

        Task AddAsync(AppUser appUser);
        void AddAsync(Contact contact);
        object Find(int productId);
        object GetAllAsync(string v);
        object GetAllAsync();
        Task<AppUser> GetAsync(Func<object, bool> value);
        object GetQueryable();
        int SaveChanges();
        Task SaveChangesAsync();
        void Update(AppUser user);
    }
}
