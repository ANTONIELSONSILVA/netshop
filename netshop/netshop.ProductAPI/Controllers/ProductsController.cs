using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netshop.ProductAPI.DTOs;
using netshop.ProductAPI.Services;

namespace netshop.ProductAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }


    [HttpGet]
    public async Task <ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var productsDto = await productService.GetProducts();
        if (productsDto is null)
            return NotFound("Products not found");
        return Ok(productsDto);

    }


    [HttpGet("{id:int}", Name = "GetProducts")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        var productsDto = await productService.GetProductById(id);
        if(productsDto == null)
        {
            return NotFound("Products not found");
        }
        return Ok(productsDto);

    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] ProductDTO productDto)
    {
        if (productDto == null)
            return BadRequest("Invalid Data");
        await productService.AddProduct(productDto);

        return new CreatedAtRouteResult("GetProducts", new { id = productDto.Id },
            productDto);
    }


    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] ProductDTO productDto)
    {
        if (productDto == null)
            return BadRequest();

        await productService.UpdateProduct(productDto);
        return Ok(productDto);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Delete (int id)
    {
        var productDto = await productService.GetProductById(id);
        if(productDto == null)
        {
            return NotFound("Product not found");
        }

        await productService.RemoveProduct(id);
        return Ok(productDto);
    }


}
