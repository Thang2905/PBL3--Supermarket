using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class Receipt
{
    [Key]
    public Guid Id { get; set; }

    // Many-to-One: Receipt → Customer
    [ForeignKey("CustomerId")]
    public Guid CustomerId { get; set; }
    [Required]
    public virtual Customer Customer { get; set; } = null!;

    // One-to-Many: Receipt → ReceiptProduct
    [Required]
    public virtual ICollection<ReceiptProduct> ReceiptProducts { get; set; } = new List<ReceiptProduct>();

    public float TotalPrice { get; set; }
    public DateTime BillDate { get; set; }
    public TimeSpan BillTime { get; set; }

    public void AddReceiptProduct(ReceiptProduct receiptProduct)
    {
        if (receiptProduct != null)
        {
            receiptProduct.Receipt = this;
            ReceiptProducts.Add(receiptProduct);
        }
    }

    public void setCustomer(Customer customer)
    {
        this.Customer = customer;
        if (customer != null)
        {
            customer.AddReceipt(this);
        }
    }
}