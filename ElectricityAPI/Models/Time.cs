using System;
using System.Collections.Generic;

namespace ElectricityAPI.Models;

public partial class Time
{
    public int Id { get; set; }

    public int HourFrom { get; set; }

    public int MinutesFrom { get; set; }

    public int HourTo { get; set; }

    public int MinutesTo { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
