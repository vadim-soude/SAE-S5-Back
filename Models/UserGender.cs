using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models;

public partial class UserGender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
