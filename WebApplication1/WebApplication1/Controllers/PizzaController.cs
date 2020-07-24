using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Entities;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaController(IConfiguration config, IPizzaRepository pizzaRepository)
        {
            _configuration = config;
            _pizzaRepository = pizzaRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> Get()
        {
            try
            {
                return Ok(_pizzaRepository.GetPizza());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }


        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            try
            {
                return Ok(_pizzaRepository.Get(id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }
    }
}