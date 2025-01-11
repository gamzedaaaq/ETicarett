using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.WebUI.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IService<Product> _service;


        public FavoritesController(IService<Product> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var favoriler = GetFavorites();
            return View(favoriler);
        }
        private List<Product> GetFavorites() 
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavories") ?? [];
        }
        public IActionResult Add(int ProductId)
        {
            var favoriler = GetFavorites();
            var product=_service.Find( ProductId);
            if (product != null && !favoriler.Any(p=>p.Id==ProductId)) 
            {
                favoriler.Add((Product)product);
                HttpContext.Session.SetJson("GetFavories",favoriler);
            }
            return RedirectToAction("Index");
        }  public IActionResult Remove(int ProductId)
        {
            var favoriler = GetFavorites();
            var product=_service.Find( ProductId);
            if (product != null && favoriler.Any(p=>p.Id==ProductId)) 
            {
                favoriler.RemoveAll(i=>i.Id==ProductId );
                HttpContext.Session.SetJson("GetFavories",favoriler);
            }
            return RedirectToAction("Index");
        }

    }
}
