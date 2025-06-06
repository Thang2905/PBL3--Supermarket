using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class CartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    // One-to-One: CartItem → Product
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    [Required]
    public virtual Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    // Many-to-One: CartItem → Cart
    [ForeignKey("Cart")]
    public Guid CartId { get; set; }
    [Required]
    public virtual Cart Cart { get; set; } = null!;
}