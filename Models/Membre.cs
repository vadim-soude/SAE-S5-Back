using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models
{
    public partial class Membre
    {
        public Membre()
        {
            EventInscriptions = new HashSet<EventInscription>();
        }

        public int IdMembre { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MailUpjv { get; set; }
        public string? DiscordUsername { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PpImageUrl { get; set; }
        public string? Description { get; set; }
        public string? Password { get; set; }
        public string? Statut { get; set; }

        public virtual ICollection<EventInscription> EventInscriptions { get; set; }
    }
}
