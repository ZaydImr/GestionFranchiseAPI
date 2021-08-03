using System;
using System.Collections.Generic;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class Produit
    {
        public int IdProduit { get; set; }
        public string NameProduit { get; set; }
        public int IdFranchise { get; set; }

        public virtual Franchise IdFranchiseNavigation { get; set; }
    }
}
