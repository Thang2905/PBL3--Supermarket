using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Admin.Enums;

namespace Admin.Models;

public partial class Delivery
{
    [Key]
    [ForeignKey(nameof(Bill))]
    public Guid BillId { get; set; }
    public virtual Receipt Bill { get; set; } = null!;

    [ForeignKey(nameof(Staff))]
    public Guid StaffId { get; set; }
    public virtual Staff Staff { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string Status { get; set; } = EnumHelper.GetDescription(OrderStatus.PROCESSING);
}