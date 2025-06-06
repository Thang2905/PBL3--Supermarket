using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class StaffDateKey
{
    [Required]
    public string StaffId { get; set; } = null!;
    public DateTime Date { get; set; }
}