using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models
{
    public partial class Actualité
    {
        public int IdActu { get; set; }
        public string? Nom { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? Auteur { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? DateCreation { get; set; }
    }
}
