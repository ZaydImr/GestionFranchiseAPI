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
    public class ProduitsController : ControllerBase
    {

        // GET all the Produits by Franchise
        [HttpGet("{idFranchise}")]
        public IEnumerable<Produit> Get(int idFranchise)
        {
            return new GestionFranchiseContext().Produits.Where(prod=>prod.IdFranchise == idFranchise).ToList();   
        }

        // POST Ajouter les produits pour un seul franchise
        [HttpPost]
        public Produit Post([FromBody] Produit produit)
        {
            var db = new GestionFranchiseContext();

            List<Produit> produits = db.Produits.Where(prod=>prod.NameProduit==produit.NameProduit && prod.IdFranchise == produit.IdFranchise).ToList();
            foreach(var pr in produits)
            {
                return null;
            }

            db.Add(produit);
            db.SaveChanges();

            return produit;
        }


        // POST Ajouter les produits pour un seul franchise
        [HttpPost("{login}")]
        public void Post(string login,[FromBody] Produit produit)
        {
            var db = new GestionFranchiseContext();
            List<Utilisateur> utilisateurs = db.Utilisateurs.Where(util=>util.Login == login && util.TypeUtilisateur == "Administrateur").ToList();
            foreach(Utilisateur util in utilisateurs)
            {
                List<Franchise> franchises = db.Franchises.ToList();
                foreach (Franchise franchise in franchises)
                {
                    produit.IdFranchise = franchise.IdFranchise;
                    List<Produit> produits = db.Produits.Where(prod => prod.NameProduit == produit.NameProduit && prod.IdFranchise == produit.IdFranchise).ToList();
                    if (produits.Count == 0)
                        db.Add(produit);
                }
                db.SaveChanges();
            }
        }


        // PUT mise a jour pour un produit
        [HttpPut("{login}")]
        public void Put(string login,[FromBody] Produit produitF)
        {
            var db = new GestionFranchiseContext();

            int id = produitF.IdProduit;
            Produit produitB = db.Produits.FirstOrDefault(prod => prod.IdProduit == id);

            db = new GestionFranchiseContext();
            db.Update(produitF);

            Command com = new Command();
            com.IdProduit = produitF.IdProduit;
            com.Login = login;
            com.QteModified = produitF.QteProduit - produitB.QteProduit;
            com.DateCommand = DateTime.UtcNow;

            db.Commands.Add(com);
            db.SaveChanges();
        }

        // DELETE suppression d'un produit
        [HttpDelete("{login}+{id}")]
        public void Delete(string login,int id)
        {
            Produit produit = null;
            var db = new GestionFranchiseContext();
            List<Utilisateur> utilisateurs = db.Utilisateurs.Where(util=>util.Login == login).ToList();
            List<Produit> produits = db.Produits.Where(prod => prod.IdProduit == id).ToList();
            foreach (Produit prod in produits)
            {
                produit = prod;
            }
            if(produit !=null)
            {
                foreach (Utilisateur utilisateur in utilisateurs)
                {
                    if (utilisateur.TypeUtilisateur == "Franchise" || utilisateur.TypeUtilisateur == "Administrateur")
                    {
                        if (produit.IdFranchise == utilisateur.IdType || utilisateur.IdType == 0)
                        {
                            db.Produits.Remove(produit);
                        }
                    }
                }
            }

            db.SaveChanges();
        }
    }
}
