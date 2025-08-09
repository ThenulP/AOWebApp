using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models;

public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Item name cannot exceed 100 characters.")]
    public string ItemName { get; set; } = null!;

    [Required]
    [StringLength(1000, ErrorMessage = "Item description cannot exceed 1000 characters.")]
    public string ItemDescription { get; set; } = null!;

    [Range(0.01, double.MaxValue, ErrorMessage = "Item cost must be greater than 0.")]
    public decimal ItemCost { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters.")]
    public string ItemImage { get; set; } = null!;

    [Required]
    public int CategoryId { get; set; }

    public virtual ItemCategory Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new HashSet<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new HashSet<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
}
