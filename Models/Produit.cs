using System;
using System.Collections.Generic;

namespace WorkshopAPI.Models
{
    public partial class Produit
    {
        public int IdProduit { get; set; }
        public string? Nom { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double? PrixAdherent { get; set; }
        public double? PrixNonAdherent { get; set; }
        public int? Stock { get; set; }
        public string? Categorie { get; set; }
        public double? PrixFournisseur { get; set; }
    }
}
