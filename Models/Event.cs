using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Startdate { get; set; }

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int NbPeople { get; set; }

    public int IdUser { get; set; }

    public int IdSport { get; set; }

    public int IdLevel { get; set; }

    public int IdEventGender { get; set; }

    public virtual EventGender IdEventGenderNavigation { get; set; } = null!;

    public virtual Level IdLevelNavigation { get; set; } = null!;

    public virtual Sport IdSportNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<User> Ids { get; set; } = new List<User>();
}
