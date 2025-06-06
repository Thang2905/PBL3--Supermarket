using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public partial class Staff : User
{
    public static float BaseSalary { get; set; }

    public float SalaryCoefficient { get; set; }

    // One-to-Many: Staff → Delivery
    [Required]
    public ICollection<Delivery> DeliveryList { get; set; } = new List<Delivery>();

    public float GetFinalSalary()
    {
        return BaseSalary * SalaryCoefficient;
    }
}