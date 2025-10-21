using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string? PaymentMethod1 { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
