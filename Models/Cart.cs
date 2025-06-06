using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class Cart
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // One-to-Many: Cart → CartItems
    public virtual ICollection<CartItem> CartItemList { get; set; } = new List<CartItem>();

    // One-to-One: Cart → Customer
    [Required]
    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public void setCustomer(Customer customer)
    {
        this.Customer = customer;
        if (customer != null)
        {
            customer.SetCart(this);
        }
    }
}
