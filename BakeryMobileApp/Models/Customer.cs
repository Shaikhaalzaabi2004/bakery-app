using System;
using System.Collections.Generic;

namespace BakeryMobileApp.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Age { get; set; }

    public string? PostalCode { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? JoinDate { get; set; }

    public string? Churned { get; set; }

    public int? GenderId { get; set; }

    public int? MemberShipStatusId { get; set; }

    public double? Balance { get; set; }

    public string? Password { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual MemberShipStatus? MemberShipStatus { get; set; }

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
