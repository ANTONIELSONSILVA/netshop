using System.IO.Pipes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using netShop.Web.Models;
using netShop.Web.Services.Contracts;

namespace netShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductServices _productsServices;
    private readonly ICategoryService _categoryService;
    private readonly IHttpClientFactory _ClientFactory;

    public ProductsController(IProductServices productsServices,
                                ICategoryService categoryService)
    {
        _productsServices = productsServices;
        _categoryService = categoryService;
    }


    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productsServices.GetAllProducts();

        if (result == null)
            return View("Error");


        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {

        if (ModelState.IsValid)
        {
            var result = await _productsServices.CreateProduct(productVM);
            if (result != null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        }

        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");

        var result = await _productsServices.FindProductById(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productsServices.UpdateProduct(productVM);
            if (result != null) return RedirectToAction(nameof(Index));
        }

        return View(productVM);

    }


    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var result = await _productsServices.FindProductById(id);

        if(result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _productsServices.DeleteProductById(id);

        if(!result)
            return View("Error");

        return RedirectToAction("Index");

    }

}