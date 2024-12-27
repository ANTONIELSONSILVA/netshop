using AutoMapper;
using netshop.ProductAPI.DTOs;
using netshop.ProductAPI.models;
using netshop.ProductAPI.Repositories;

namespace netshop.ProductAPI.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var productsEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(productsEntity);
    }

    public async Task AddProduct(ProductDTO productDTO)
    {
        var productsEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Create(productsEntity);
        productDTO.Id = productsEntity.Id;
    }

    public async Task UpdateProduct(ProductDTO productDTO)
    {
        var productsEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Update(productsEntity);
    }

    public async Task RemoveProduct(int id)
    {
        var productsEntity = await _productRepository.GetById(id);
        await _productRepository.Delete(productsEntity.Id);
    }
}
