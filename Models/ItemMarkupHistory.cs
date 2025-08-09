using System;
using System.Collections.Generic;

namespace AOWebApp.Models;

public partial class ItemMarkupHistory
{
    public int ItemId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal Markup { get; set; }

    public bool Sale { get; set; }

    public virtual Item Item { get; set; } = null!;
}
