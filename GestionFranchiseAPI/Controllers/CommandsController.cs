using GestionFranchiseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionFranchiseAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {

        // GET all commands
        [HttpGet]
        public IEnumerable<Command> Get()
        {
            return new GestionFranchiseContext().Commands.ToList();   
        }

        // GET all commandes of one franchise
        [HttpGet("{franchise}")]
        public IEnumerable<Command> Get(string franchise)
        {
            var db = new GestionFranchiseContext();
            List<Command> commands = new List<Command>();
            Franchise fr = db.Franchises.FirstOrDefault(franch => franch.Login == franchise);
            List<Produit> produits = db.Produits.Where(prod => prod.IdFranchise == fr.IdFranchise).ToList();
            db = new GestionFranchiseContext();
            foreach (Produit produit in produits)
            {
                commands.AddRange(db.Commands.Where(com => com.IdProduit == produit.IdProduit).ToList());
            }
            return commands;
        }
    }
}
