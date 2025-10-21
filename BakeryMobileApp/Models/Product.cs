using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public double? Cost { get; set; }

    public string? Seasonal { get; set; }

    public string? Active { get; set; }

    public DateTime? IntroducedDate { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? PhotoName { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductsIngredient> ProductsIngredients { get; set; } = new List<ProductsIngredient>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
