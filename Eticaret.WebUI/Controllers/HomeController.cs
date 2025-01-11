using Eticaret.Core.Entities;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eticaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Slider> _serviceSlider;
        private readonly IService<Contact> _serviceContact;


        public HomeController(IService<Product> _serviceProduct, IService<Slider> serviceSlider, IService<Contact> serviceContact)
        {
            _serviceProduct = _serviceProduct;
            _serviceSlider = serviceSlider;
            _serviceContact = serviceContact;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _serviceSlider.GetAllAsync(); 
                Products = await _serviceProduct.GetAsync(p => p.IsActive && p.IsHome); 
            }; 

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _serviceContact.AddAsync(contact);
                        var sonuc = _serviceContact.SaveChanges();
                    if (sonuc < 0)
                    {
                        TempData["message"] = "<div class='alert alert-success'>Mesajýnýz Gönderilmiþtir</div>";
                        return RedirectToAction("ContactUs");
                    }
                }
                catch (Exception ) {
                    ModelState.AddModelError("", "hata oluþtu");
                
                }


            }
            return View(contact);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
