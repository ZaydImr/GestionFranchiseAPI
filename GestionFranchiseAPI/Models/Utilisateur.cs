using System;
using System.Collections.Generic;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Commands = new HashSet<Command>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string NameUtilisateur { get; set; }
        public string NumUtilisateur { get; set; }
        public string EmailUtilisateur { get; set; }
        public string TypeUtilisateur { get; set; }
        public int? IdType { get; set; }

        public virtual ICollection<Command> Commands { get; set; }
    }
}
