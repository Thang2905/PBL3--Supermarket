using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

[Table("Bill_Products")]
public partial class ReceiptProduct
{
    [Key, Column(Order = 0)]
    public Guid ReceiptId { get; set; }

    [Key, Column(Order = 1)]
    public Guid ProductId { get; set; }

    // Navigation properties
    [Required]
    [ForeignKey("ReceiptId")]
    public virtual Receipt Receipt { get; set; } = null!;

    [Required]
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    public ReceiptProduct() {}

    public ReceiptProduct(Receipt receipt, Product product, int quantity)
    {
        ReceiptId = receipt.Id;
        ProductId = product.Id;
        Receipt = receipt;
        Product = product;
        Quantity = quantity;
    }
}