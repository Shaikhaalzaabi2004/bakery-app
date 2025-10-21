using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class Channel
{
    public int Id { get; set; }

    public string? Channel1 { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
