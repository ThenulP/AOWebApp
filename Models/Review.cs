using System;
using System.Collections.Generic;

namespace AOWebApp.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly ReviewDate { get; set; }

    public int ItemId { get; set; }

    public int Rating { get; set; }

    public string ReviewDescription { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
