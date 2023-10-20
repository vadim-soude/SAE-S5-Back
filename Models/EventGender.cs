using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models;

public partial class EventGender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
