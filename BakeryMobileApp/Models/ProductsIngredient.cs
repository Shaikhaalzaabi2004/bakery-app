using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BakeryMobileApp.Models;

public partial class ProductsIngredient
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? IngredientId { get; set; }

    public virtual Ingredient? Ingredient { get; set; }
    [JsonIgnore]
    public virtual Product? Product { get; set; }
}
