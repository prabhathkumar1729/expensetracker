using System;
using System.Collections.Generic;

namespace BlazorWalletTrackerDAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string SecurityQuestion { get; set; } = null!;

    public string SecurityAnswer { get; set; } = null!;

    public bool? IsUserActive { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
