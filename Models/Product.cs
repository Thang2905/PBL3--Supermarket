using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Admin.Models;

public partial class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    [JsonPropertyName("quantity")]
    public int StockQuantity { get; set; }
    [Required]
    public float Price { get; set; }
    [JsonPropertyName("unit_price")]
    public string UnitPrice { get; set; } = null!;
    public float? Discount { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    // Many-to-Many: Product <-> Category
    [Required]
    public virtual Category Categories { get; set; } = null!;

    // Many-to-One: Product → Suppplier
    [ForeignKey("SupplierId")]
    public Guid SupplierId { get; set; }
    [Required]
    public virtual Supplier Supplier { get; set; } = null!;

    // One-to-Many: Product → StockImport
    public virtual ICollection<StockImport> Stocks { get; set; } = new List<StockImport>();

    public void IncreaseQuantity(int quantity) 
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be a positive number.", nameof(quantity));
        }

        this.StockQuantity += quantity;
    }
}