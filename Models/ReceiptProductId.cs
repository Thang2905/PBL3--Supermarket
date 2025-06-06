using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class ReceiptProductId : IEquatable<ReceiptProductId>
{
    public Guid ReceiptId { get; set; }
    public Guid ProductId { get; set; }

    public ReceiptProductId() { }

    public ReceiptProductId(Guid receiptId, Guid productId)
    {
        ReceiptId = receiptId;
        ProductId = productId;
    }

    public bool Equals(ReceiptProductId? other)
    {
        if (other == null) return false;
        return ReceiptId.Equals(other.ReceiptId) && ProductId.Equals(other.ProductId);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ReceiptProductId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ReceiptId, ProductId);
    }
}