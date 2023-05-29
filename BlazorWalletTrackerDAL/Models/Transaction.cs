using System;
using System.Collections.Generic;

namespace BlazorWalletTrackerDAL.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string Category { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual User User { get; set; } = null!;

    //public override string ToString()
    //{
    //    return $"\n This is DAL transaction model \n TransactionId= {this.TransactionId}, UserId={UserId}, TransactionDate={this.TransactionDate},  Amount={this.Amount},  Category={this.Category}, IsActive={false}\n";
    //}

    //override the tostring method to print all the properties of the transaction object
    public override string ToString()
    {
        return $"\n This is DAL transaction model \n TransactionId= {TransactionId}, UserId={UserId}, TransactionDate={TransactionDate},  Amount={Amount},  Category={Category}, IsActive={false}\n";
    }   



}

