using System;
using System.Collections.Generic;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class Command
    {
        public int IdCommand { get; set; }
        public int IdProduit { get; set; }
        public string Login { get; set; }
        public int QteModified { get; set; }
        public DateTime? DateCommand { get; set; }

        public virtual Produit IdProduitNavigation { get; set; }
        public virtual Utilisateur LoginNavigation { get; set; }
    }
}
