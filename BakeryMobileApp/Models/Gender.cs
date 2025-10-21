using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
