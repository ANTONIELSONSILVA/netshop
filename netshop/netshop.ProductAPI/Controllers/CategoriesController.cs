using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using netshop.ProductAPI.DTOs;
using netshop.ProductAPI.Services;
using netshop.ProductAPI.Roles;

namespace netshop.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var cartegoriesDto = await _categoryService.GetCategories();
            if (cartegoriesDto is null)
                return NotFound("Categories not found");

            return Ok(cartegoriesDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategorysProducts()
        {
            var categoriesDto = await _categoryService.GetCategoriesProducts();
            if(categoriesDto == null)
            {
                return NotFound("Categories not found");
            }

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);
            if(categoryDto == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Invalid Data");

            await _categoryService.AddCategory(categoryDto);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId },
                categoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
                return BadRequest();

            if (categoryDto == null)
                return BadRequest();

            await _categoryService.UpdateCategory(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);
            if (categoryDto == null)
            {
                return NotFound("Category not found");
            }

            await _categoryService.RemoveCategory(id);
            return Ok(categoryDto);

        }

    }

}
