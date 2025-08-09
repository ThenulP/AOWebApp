using System;
using System.Collections.Generic;

namespace AOWebApp.Models;

public partial class CustomerOrder
{
    public int OrderNumber { get; set; }

    public int CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly? DatePaid { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();
}
