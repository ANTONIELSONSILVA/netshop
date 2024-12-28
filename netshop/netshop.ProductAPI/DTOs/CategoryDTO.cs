using System.ComponentModel.DataAnnotations;
using netshop.ProductAPI.models;

namespace netshop.ProductAPI.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(300)]
    public string? Name { get; set; }


    public ICollection<Product>? Products { get; set; }
}
