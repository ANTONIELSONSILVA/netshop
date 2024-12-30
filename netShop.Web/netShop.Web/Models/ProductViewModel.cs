using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace netShop.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public string? ImageURL { get; set; }

    [Display(Name= "Categorias")]
    public string? CategoryName { get; set; }
    public int CategoryId { get; set; }
}
