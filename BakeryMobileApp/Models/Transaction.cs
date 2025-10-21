using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? Time { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public double? Discount { get; set; }

    public int? PaymentMethodId { get; set; }

    public int? ChannelId { get; set; }

    public string? Address { get; set; }

    public virtual Channel? Channel { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual Product? Product { get; set; }
    public decimal? TotalValue => Math.Round((decimal)Price * (decimal)Quantity, 2);

}
