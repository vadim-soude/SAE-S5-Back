using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models;

public partial class SportCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sport> Sports { get; set; } = new List<Sport>();
}
