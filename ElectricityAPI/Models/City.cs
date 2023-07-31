using System;
using System.Collections.Generic;

namespace ElectricityAPI.Models;

public partial class City
{
    public int Id { get; set; }

    public string City1 { get; set; } = null!;

    public int GovId { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual Governorate Gov { get; set; } = null!;
}
