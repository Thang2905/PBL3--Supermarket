using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public class WorkingDate
{
    [Required]
    [ForeignKey("Staff")]
    public string StaffId { get; set; } = null!;
    
    // Many-to-One: Working_Date → Staff
    [Required]
    [ForeignKey("StaffId")]
    public Staff Staff { get; set; } = null!;
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }

    public int NumberOfDeliveries { get; set; }
}