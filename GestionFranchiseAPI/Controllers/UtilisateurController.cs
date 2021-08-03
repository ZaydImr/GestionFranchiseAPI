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
    public class UtilisateurController : ControllerBase
    {
        // GET all the users
        [HttpGet]
        public IEnumerable<Utilisateur> Get()
        {
            return new GestionFranchiseContext().Utilisateurs.ToList();   
        }

        // GET a specified user by his login and his password
        [HttpGet("{login}+{password}")]
        public IEnumerable<Utilisateur> Get(string login,string password)
        {
            return new GestionFranchiseContext().Utilisateurs.Where(util=>util.Login == login && util.Password == password ).ToList();
        }

        // GET all the users by a specified type
        [HttpGet("{type}")]
        public IEnumerable<Utilisateur> Get(string type)
        {
            return new GestionFranchiseContext().Utilisateurs.Where(util => util.TypeUtilisateur == type).ToList();
        }

        // POST api/<LoginController>
        [HttpPost]
        public void Post([FromBody] Utilisateur utilisateur)
        {
            if(utilisateur.TypeUtilisateur == "Administrateur")
            {
                utilisateur.IdType = null;
                new GestionFranchiseContext().Add(utilisateur);
            }
            else if(utilisateur.TypeUtilisateur == "Franchise")
            {
                new GestionFranchiseContext().Add(utilisateur);
                Franchise fr = new Franchise();
                fr.Login = utilisateur.Login;
                new GestionFranchiseContext().Add(fr);
            }
            else if (utilisateur.TypeUtilisateur == "Agent")
            {
                new GestionFranchiseContext().Add(utilisateur);
                Franchise fr = new Franchise();
                fr.Login = utilisateur.Login;
                new GestionFranchiseContext().Add(fr);
            }

        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
