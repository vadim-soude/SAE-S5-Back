using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models
{
    public partial class Event
    {
        public Event()
        {
            EventInscriptions = new HashSet<EventInscription>();
        }

        public int IdEvent { get; set; }
        public string? Nom { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? Auteur { get; set; }
        public int? NbPlacesDispo { get; set; }
        public int? NbPlaceRestantes { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? DateCreation { get; set; }
        public string? Lieu { get; set; }

        public virtual ICollection<EventInscription> EventInscriptions { get; set; }
    }
}
