using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class Preference
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Customer? Customer { get; set; }
}
