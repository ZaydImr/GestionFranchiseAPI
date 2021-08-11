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
        public Utilisateur Get(string login,string password)
        {
            Utilisateur utilisateur = new GestionFranchiseContext().Utilisateurs.FirstOrDefault(util => util.Login == login );
            if (utilisateur != null && utilisateur.Password != password)
                utilisateur = null;
            return utilisateur;
        }
        // GET user by login
        [HttpGet("user/{login}")]
        public Utilisateur GetOne(string login)
        {
            return new GestionFranchiseContext().Utilisateurs.FirstOrDefault(util => util.Login == login);
        }

        // GET all the users by a specified type
        [HttpGet("{type}")]
        public IEnumerable<Utilisateur> Get(string type)
        {
            return new GestionFranchiseContext().Utilisateurs.Where(util => util.TypeUtilisateur == type).ToList();
        }

        // POST Ajouter les utilisateurs par type
        [HttpPost]
        public void Post([FromBody] Utilisateur utilisateur)
        {
            var db = new GestionFranchiseContext();

            if(utilisateur.TypeUtilisateur == "Administrateur")
            {
                utilisateur.IdType = 0;
                db.Add(utilisateur);
            }
            else if(utilisateur.TypeUtilisateur == "Franchise")
            {
                Franchise fr = new Franchise();
                fr.Login = utilisateur.Login;
                db.Franchises.Add(fr);
                db.SaveChanges();

                List<Franchise> f = db.Franchises.Where(fr => fr.Login == utilisateur.Login).ToList();
                foreach(Franchise ff in f)
                {
                    utilisateur.IdType = ff.IdFranchise;
                }
                db.Add(utilisateur);
            }
            db.SaveChanges();
        }


        [HttpPost("{idFranchise}")]
        public void Post(int idFranchise,[FromBody] Utilisateur utilisateur)
        {
            var db = new GestionFranchiseContext();

            if (utilisateur.TypeUtilisateur == "Agent" && idFranchise != null)
            {
                Agent ag = new Agent();
                ag.IdFranchise = idFranchise;
                ag.Login = utilisateur.Login;
                db.Agents.Add(ag);
                db.SaveChanges();

                List<Agent> f = db.Agents.Where(ag => ag.Login == utilisateur.Login).ToList();
                foreach (Agent ff in f)
                {
                    utilisateur.IdType = ff.IdAgent;
                }
                db.Add(utilisateur);
            }
            db.SaveChanges();
        }


        // PUT mise a jour pour un utilisateur
        [HttpPut]
        public void Put([FromBody] Utilisateur utilisateur)
        {
            var db = new GestionFranchiseContext();
            db.Utilisateurs.Update(utilisateur);
            db.SaveChanges();
        }

        // DELETE suppression d'un utilisateur depant au type de compte
        [HttpDelete("{login}")]
        public void Delete(string login)
        {
            var db = new GestionFranchiseContext();
            Utilisateur user = new Utilisateur();

            List<Utilisateur> u = db.Utilisateurs.Where(Utilisateur => Utilisateur.Login.Equals(login)).ToList();
            foreach (Utilisateur utilisateur in u)
            {
                user = utilisateur;
            }
            if (user.TypeUtilisateur == "Agent")
            {
                Agent ag = new Agent();
                List<Agent> agents = db.Agents.Where(agent=>agent.Login == login).ToList();
                foreach (Agent agent in agents)
                    ag = agent;
                db.Agents.Remove(ag);
            }
            else if(user.TypeUtilisateur == "Franchise")
            {
                Franchise fr = new Franchise();
                List<Franchise> franchises = db.Franchises.Where(franchise => franchise.Login == login).ToList();
                foreach (Franchise franchise in franchises)
                    fr = franchise;
                
                List<Agent> agents = db.Agents.Where(agent => agent.IdFranchise == fr.IdFranchise).ToList();
                foreach (Agent agent in agents)
                {
                    Agent ag = agent;
                    db.Agents.Remove(ag);
                    List<Utilisateur> uu = db.Utilisateurs.Where(Utilisateur => Utilisateur.Login.Equals(ag.Login)).ToList();
                    foreach (Utilisateur utilisateur in uu)
                    {
                        Utilisateur ul = utilisateur;
                        db.Utilisateurs.Remove(ul);
                    }
                }

                db.Franchises.Remove(fr);
            }

            db.Utilisateurs.Remove(user);
            db.SaveChanges();
        }
    }
}
