using System;
using System.Collections.Generic;

namespace AOWebApp.Models;

public partial class ItemCategory
{
    public int CategoryId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ItemCategory> InverseParentCategory { get; set; } = new List<ItemCategory>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ItemCategory? ParentCategory { get; set; }
}
