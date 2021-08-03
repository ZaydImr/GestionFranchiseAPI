using System;
using System.Collections.Generic;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class Franchise
    {
        public Franchise()
        {
            Agents = new HashSet<Agent>();
            Produits = new HashSet<Produit>();
        }

        public int IdFranchise { get; set; }
        public string Login { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Produit> Produits { get; set; }
    }
}
