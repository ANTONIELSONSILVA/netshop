using AutoMapper;
using netshop.ProductAPI.DTOs;
using netshop.ProductAPI.models;
using netshop.ProductAPI.Repositories;

namespace netshop.ProductAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoriesEntity = await categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    {
        var categoriesEntity = await categoryRepository.GetCategoriesProducts();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);

    }

    public async Task<CategoryDTO> GetCategoryById(int id)
    {
        var categoriesEntity = await categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO>(categoriesEntity);
    }

    public async Task AddCategory(CategoryDTO categoryDTO)
    {
        var categoriesEntity = _mapper.Map<Category>(categoryDTO);
        await categoryRepository.Create(categoriesEntity);
        categoryDTO.CategoryId = categoriesEntity.CategoryId;
    }


    public async Task UpdateCategory(CategoryDTO categoryDTO)
    {
        var categoriesEntity = _mapper.Map<Category>(categoryDTO);
        await categoryRepository.Update(categoriesEntity);
    }

    public async Task RemoveCategory(int id)
    {
        var categoriesEntity = categoryRepository.GetById(id).Result;
        await categoryRepository.Delete(categoriesEntity.CategoryId);
    }

}
