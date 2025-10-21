using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BakeryMobileApp.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
