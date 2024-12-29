using Microsoft.AspNetCore.Mvc;
using netShop.Web.Models;
using netShop.Web.Services.Contracts;

namespace netShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _productsServices;

        public ProductsController(IProductServices productsServices)
        {
            _productsServices = productsServices;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var result = await _productsServices.GetAllProducts();

            if (result == null)
                return View("Error");


            return View(result);
        }
    }
}
