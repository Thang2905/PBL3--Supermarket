using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class Supplier
{
    [Key]
    public Guid? Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    // One-to-Many: Supplier → Product
    [Required]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}