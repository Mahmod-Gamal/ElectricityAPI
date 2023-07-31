using System;
using System.Collections.Generic;

namespace ElectricityAPI.Models;

public partial class Governorate
{
    public int Id { get; set; }

    public string Gov { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
