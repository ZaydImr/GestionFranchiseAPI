using System;
using System.Collections.Generic;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class Produit
    {
        public Produit()
        {
            Commands = new HashSet<Command>();
        }

        public int IdProduit { get; set; }
        public string NameProduit { get; set; }
        public int IdFranchise { get; set; }
        public int QteProduit { get; set; }

        public virtual Franchise IdFranchiseNavigation { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
    }
}
