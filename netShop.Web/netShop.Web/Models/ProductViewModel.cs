﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace netShop.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public int Stock { get; set; }
    public string? ImageURL { get; set; }


    public string? CategoryName { get; set; }
    public int CategoryId { get; set; }
}
