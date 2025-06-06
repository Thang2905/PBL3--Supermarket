using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class StockImport
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // Many-to-One: StockImport → Product
    [ForeignKey("ProductId")]
    public Guid ProductId { get; set; }
    [Required]
    public virtual Product Product { get; set; } = null!;

    public int ImportQuantity { get; set; }

    public float ImportPrice { get; set; }

    public DateTime ImportDate { get; set; }

    public TimeSpan ImportTime { get; set; }

    public float GetTotalPrice()
    {
        return ImportPrice * ImportQuantity;
    }
}