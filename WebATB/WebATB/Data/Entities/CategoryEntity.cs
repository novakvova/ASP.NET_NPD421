﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebATB.Data.Entities;

[Table("tblCategories")]
public class CategoryEntity
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [StringLength(200)]
    public string? Image { get; set; }

    public virtual ICollection<ProductEntity>? Products { get; set; }
}
