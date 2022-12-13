using System;
using System.Collections.Generic;

namespace App.DAL.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
