using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models
{
    public partial class EventInscription
    {
        public int IdEventInscription { get; set; }
        public bool? StatutPaiement { get; set; }
        public int IdMembre { get; set; }
        public int IdEvent { get; set; }

        public virtual Event IdEventNavigation { get; set; } = null!;
        public virtual Membre IdMembreNavigation { get; set; } = null!;
    }
}
