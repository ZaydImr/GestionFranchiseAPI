using System;
using System.Collections.Generic;

#nullable disable

namespace GestionFranchiseAPI.Models
{
    public partial class Agent
    {
        public int IdAgent { get; set; }
        public int IdFranchise { get; set; }

        public virtual Franchise IdFranchiseNavigation { get; set; }
    }
}
