using System;
using System.Collections.Generic;

namespace ElectricityAPI.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Area1 { get; set; } = null!;

    public int CityId { get; set; }

    public int TimeId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Time Time { get; set; } = null!;
}
