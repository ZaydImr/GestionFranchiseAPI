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
    public class AgentController : ControllerBase
    {

        // GET all the users
        [HttpGet("{login}")]
        public Agent Get(string login)
        {
            return new GestionFranchiseContext().Agents.FirstOrDefault(agent=>agent.Login==login);   
        }
    }
}
