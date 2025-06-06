using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class Customer : User
{
    public int Point {get; set; }

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual Cart Cart { get; set; } = null!;

    public void SetCart(Cart cart)
    {
        this.Cart = cart;
        if (cart != null)
        {
            cart.setCustomer(this);
        }
    }

    public void AddReceipt(Receipt receipt)
    {
        this.Receipts.Add(receipt);
        if (receipt != null)
        {
            receipt.setCustomer(this);
        }
    }
}