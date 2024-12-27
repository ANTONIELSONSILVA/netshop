using Microsoft.EntityFrameworkCore;
using netshop.ProductAPI.Context;
using netshop.ProductAPI.models;

namespace netshop.ProductAPI.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.products.ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        return await _context.products.Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product> Create(Product product)
    {
        _context.products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Delete(int id)
    {
        var product = await GetById(id);
        _context.products.Remove(product);
        await _context.SaveChangesAsync();
        return product;

    }

}
