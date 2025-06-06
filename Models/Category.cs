using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? Name { get; set; } = null!;

    [Column(TypeName = "nvarchar(200)")]
    public string? Description { get; set; } = null!;
    
    // Many-to-Many: Category <-> Product
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}