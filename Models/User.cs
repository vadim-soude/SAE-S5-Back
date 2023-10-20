using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public bool Disabled { get; set; }

    public bool Expert { get; set; }

    public int IdUserGender { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual UserGender IdUserGenderNavigation { get; set; } = null!;

    public virtual ICollection<Event> IdEvents { get; set; } = new List<Event>();
}
