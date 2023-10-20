using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models;

public partial class Sport
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdSportCategory { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual SportCategory IdSportCategoryNavigation { get; set; } = null!;
}
