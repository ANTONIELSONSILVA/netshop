using netShop.Web.Models;

public class CategoryViewModel
{
    public int CategoryId { get; set; } // Corrigido de 'CateogryId' para 'CategoryId'
    public string? Name { get; set; }
    public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>(); // Evita nulo
}
